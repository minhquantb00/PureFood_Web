using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.Server;
using PureFood.BaseApplication.HostedServices;
using PureFood.BaseApplication.Middlewares;
using PureFood.BaseApplication.Services;
using PureFood.BaseRepositories;
using PureFood.Cache;
using PureFood.Common;
using PureFood.Config;
using PureFood.EventBus;
using PureFood.GrpcClient;
using PureFood.GrpcServer;
using PureFood.HttpClientBase;
using Serilog;
using Serilog.Events;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

public abstract class BaseProgram
{
    public static string? HostName;
    public static string? HostIp;

    public static void Run(string[] args, Func<IServiceCollection, IServiceCollection>? registerServiceFunc, Action<WebApplication>? registerRoutingUrl,  Func<HttpContext, string>? getWebsiteIdByRequest = null)
    {
        HostName = Dns.GetHostName();
        Console.WriteLine($"HostName: {HostName}");

        var t = Dns.GetHostEntry(HostName);
        foreach (var address in t.AddressList)
        {
            string ip = address.ToString();
            if(address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                HostIp = ip;
            }

            Console.WriteLine($"IP Address is: {ip}");
        }

        Console.WriteLine($"HostIp: {HostIp}");
        Console.WriteLine("Build Version: BUILD_VERSION");

        ThreadPool.GetMinThreads(out var minWorker, out var minIoc);
        ThreadPool.GetMaxThreads(out var maxWorker, out var maxIoc);

        Console.WriteLine($"minWorker: {minWorker}, minIOC: {minIoc}");
        Console.WriteLine($"maxWorker: {maxWorker}, minIOC: {maxIoc}");

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            // WebRootPath = string.Empty,
            Args = args
        });

        ConfigSetting.Init(builder.Configuration);
        string appName = ConfigSettingEnum.AppName.GetConfig() ?? "PureFood.BaseApplication";

        string appVersion = ConfigSettingEnum.AppVersion.GetConfig() ?? "PureFood.BaseApplication";

        LogEventLevel logEventLevel = (LogEventLevel)ConfigSettingEnum.LogEventLevel.GetConfig().AsInt();

        string logEsUrl = ConfigSettingEnum.LogToEsUrl.GetConfig();

        string logPath = Path.Combine(Environment.CurrentDirectory, "log");

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", logEventLevel)
            .Enrich.WithProperty("HostName", HostName)
            .Enrich.WithProperty("AppName", appName)
            .Enrich.WithProperty("AppVersion", appVersion)
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate:
                "[{Level} {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] {Message} {Properties}{NewLine}{Exception}",
                restrictedToMinimumLevel: logEventLevel)
            .WriteTo.File(
                $"{logPath}/log-.txt",
                fileSizeLimitBytes: 1_000_000,
                rollOnFileSizeLimit: true,
                shared: false,
                flushToDiskInterval: TimeSpan.FromSeconds(5),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: logEventLevel,
                buffered: true
            );

        Log.Logger = loggerConfiguration.CreateLogger();
        int httpPort = ConfigSettingEnum.HttpPort.GetConfig().AsInt();
        if (httpPort <= 0)
        {
            httpPort = 30000;
        }

        builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
        builder.Host.UseSerilog();
        int httpType = ConfigSettingEnum.HttpType.GetConfig().AsInt();
        if (httpType != 2 && httpType != 3)
        {
            httpType = 1;
        }

        builder.WebHost.UseKestrel(options =>
        {
            options.AllowSynchronousIO = true;
            options.Limits.MinRequestBodyDataRate = null;
            options.Limits.MaxRequestBodySize = 50971520000;
            options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(60);
            options.Limits.MaxConcurrentConnections = int.MaxValue;
            options.Limits.MaxConcurrentUpgradedConnections = int.MaxValue;
            options.Limits.MaxRequestBufferSize = null; // no limit!
            options.Limits.MaxResponseBufferSize = null; // no limit!
            var http2 = options.Limits.Http2;
            http2.InitialConnectionWindowSize = 2 * 1024 * 1024 * 2;
            http2.InitialStreamWindowSize = 1024 * 1024;
            http2.MaxStreamsPerConnection = int.MaxValue;
            if (httpType == 1)
            {
                options.ListenAnyIP(httpPort,
                    listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });
            }
            else if (httpType == 2)
            {
                options.ListenAnyIP(httpPort,
                    listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
            }
            else if (httpType == 3)
            {
                options.ListenAnyIP(httpPort,
                    listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });
                int httpPort2 = ConfigSettingEnum.HttpPort2.GetConfig().AsInt();
                options.ListenAnyIP(httpPort2,
                    listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
            }
        });

        builder.Services.Configure<HostOptions>(options => { options.ShutdownTimeout = TimeSpan.FromMinutes(10); });
        builder.Services.AddLogging(p =>
            p.AddSerilog(Log.Logger)
        );
        builder.Services.ConfigCodeFirstGrpc();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<ContextService>();
        builder.Services.AddTransient<AuthenService>();
        builder.Services.AddSingleton<ILogAction, LogAction>();

        string dbConnectionString = ConfigSettingEnum.DbConnectionString.GetConfig();
        if (dbConnectionString.Length > 0)
        {
            builder.Services.AddTransient<IDbConnectionFactory>(p =>
            {
                var log = p.GetRequiredService<ILogger<DbConnectionFactory>>();
                return new DbConnectionFactory(dbConnectionString, log);
            });
        }

        string rabbitMqHost = ConfigSettingEnum.RabbitMqHost.GetConfig();
        if (rabbitMqHost.Length > 0)
        {
            builder.Services.AddSingleton<RabbitMqConnectionPool>(sp =>
            {
                ILogger<RabbitMqConnectionPool> logger = sp.GetRequiredService<ILogger<RabbitMqConnectionPool>>();
                int poolSize = ConfigSettingEnum.RabbitMqPoolSize.GetConfig().AsInt();
                if (poolSize <= 0)
                {
                    poolSize = 1;
                }

                return new RabbitMqConnectionPool(logger, poolSize);
            });
        }

        builder.Services.RegisterHttpClient();
        builder.Services.RegisterGrpcClientLoadBalancing();
        builder.Services.AddSingleton<RedisConnectionPersistence>(p =>
        {
            var connectionPersistence = new RedisConnectionPersistence
            (
                ConfigSettingEnum.RedisPersistenceHostIps.GetConfig(),
                ConfigSettingEnum.RedisPassword.GetConfig(),
                p.GetRequiredService<ILogger<RedisConnectionPersistence>>()
            );
            return connectionPersistence;
        });
        builder.Services.AddSingleton<RedisConnectionPool>(provider =>
        {
            var redisConnectionPool = new RedisConnectionPool
            (
                ConfigSettingEnum.RedisHostIps.GetConfig(),
                ConfigSettingEnum.RedisPassword.GetConfig(),
                ConfigSettingEnum.RedisCacheDbId.GetConfig().AsInt(),
                ConfigSettingEnum.RedisPoolSize.GetConfig().AsInt(2),
                provider.GetRequiredService<ILogger<RedisConnectionPool>>()
            );
            return redisConnectionPool;
        });
        string redisCacheEnvironment = ConfigSettingEnum.DataProtectionRedisKey.GetConfig();
        builder.Services.AddSingleton<ICacheService>(p =>
        {
            RedisConnectionPool redisConnectionPool = p.GetRequiredService<RedisConnectionPool>();
            return new RedisCacheService(redisConnectionPool, redisCacheEnvironment,
                p.GetRequiredService<ILogger<RedisCacheService>>());
        });

        var authenticationType = 2;
        if (authenticationType > 0)
        {
            IdentityModelEventSource.ShowPII = true;
            switch (authenticationType)
            {
                case 2:
                    {
                        builder.Services.AddAuthentication(options =>
                        {
                            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                            {
                                options.Cookie.HttpOnly = true;
                                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Unspecified;
                                options.Cookie.Name = !string.IsNullOrWhiteSpace(Constant.CookieAuthenName)? Constant.CookieAuthenName: "PureFood.Auth";
                                options.LoginPath = string.Empty;
                                options.LogoutPath = string.Empty;
                                options.AccessDeniedPath = string.Empty;
                                string cookieDomain = Constant.CookieDomain;
                                if (cookieDomain.Length > 0)
                                {
                                    options.Cookie.Domain = cookieDomain;
                                }

                                options.Events.OnRedirectToLogin = context =>
                                {
                                    if (authenticationType == 5)
                                    {
                                        if (context.Request.Path.StartsWithSegments("/api") &&
                                            context.Response.StatusCode == (int)HttpStatusCode.OK)
                                        {
                                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                        }
                                        else
                                        {
                                            context.Response.Redirect(context.RedirectUri);
                                        }
                                    }
                                    else if (authenticationType == 6)
                                    {
                                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                    }

                                    return Task.FromResult(0);
                                };
                            })
                            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
                            {
                                cfg.RequireHttpsMetadata = false;
                                cfg.SaveToken = true;
                                cfg.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuerSigningKey = true,
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    IssuerSigningKey =
                                        new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig())),
                                };
                                cfg.Events = new JwtBearerEvents()
                                {
                                    OnChallenge = context =>
                                    {
                                        string authorization = context.Request.Headers[HeaderNames.Authorization];
                                        if (string.IsNullOrEmpty(authorization) && context.Response.StatusCode == 302)
                                        {
                                            context.HandleResponse();
                                        }

                                        return Task.FromResult(0);
                                    },
                                };
                            })
                            .AddPolicyScheme("JwtBearer", "Cookie", options =>
                            {
                                options.ForwardDefaultSelector = context =>
                                {
                                    string? authorization = context.Request.Headers[HeaderNames.Authorization];
                                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                                    {
                                        return JwtBearerDefaults.AuthenticationScheme;
                                    }

                                    return CookieAuthenticationDefaults.AuthenticationScheme;
                                };
                            });
                        builder.Services.AddAuthorization(options =>
                        {
                            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                                JwtBearerDefaults.AuthenticationScheme,
                                CookieAuthenticationDefaults.AuthenticationScheme);
                            defaultAuthorizationPolicyBuilder =
                                defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                        });
                    }
                    break;
                case 1:
                    {
                        builder.Services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
                            {
                                cfg.RequireHttpsMetadata = false;
                                cfg.SaveToken = true;
                                cfg.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    ValidateIssuerSigningKey = true,
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    IssuerSigningKey =
                                        new SymmetricSecurityKey(
                                            Encoding.ASCII.GetBytes(ConfigSettingEnum.JwtTokensKey.GetConfig())),
                                    ValidateLifetime = true
                                };
                            });
                        builder.Services.AddAuthorization();
                    }
                    break;
            }

            // var requireAuthPolicy = new AuthorizationPolicyBuilder()
            //     .RequireAuthenticatedUser()
            //     .Build();
            // builder.Services.AddAuthorizationBuilder()
            //     .SetDefaultPolicy(requireAuthPolicy);

            string dataProtectionRedisKey =
                $"DataProtectionRedisKey_TYT_SSO_{ConfigSettingEnum.DataProtectionRedisKey.GetConfig()}";
            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var connectionPersistence = scope.ServiceProvider.GetRequiredService<RedisConnectionPersistence>();
            connectionPersistence.MakeConnection().GetAwaiter().GetResult();
            IDataProtectionBuilder dataProtectionBuilder = builder
                .Services
                .AddDataProtection()
                .SetApplicationName(dataProtectionRedisKey)
                .PersistKeysToStackExchangeRedis(
                    () => connectionPersistence.GetDatabase(Constant.RedisDbIdDataProtectionKey)!,
                    $"{dataProtectionRedisKey}")
                .UseCustomCryptographicAlgorithms(
                    new ManagedAuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithmType = typeof(Aes),
                        EncryptionAlgorithmKeySize = 256,
                        ValidationAlgorithmType = typeof(HMACSHA512)
                    });
            if (ConfigSettingEnum.AccountManagerAutomaticKeyGeneration.GetConfig().AsInt() == 1)
            {
                dataProtectionBuilder.DisableAutomaticKeyGeneration();
            }

        }

        string corsConfig = ConfigSettingEnum.CORS.GetConfig().AsEmpty();
        if (corsConfig.Length > 0)
        {
            builder.Services.AddCors(options =>
            {
                var configs = corsConfig.Split(',').Select(p => p.Trim()).ToArray();
                options.AddPolicy(Constant.CorsPolicy,
                    policyBuilder => policyBuilder.WithOrigins(configs).AllowAnyHeader().AllowCredentials()
                );
            });
        }

        builder.Services.AddControllers(options =>
        {
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        builder.Services.AddResponseCompression();

        var useSwagger = ConfigSettingEnum.UseSwagger.GetConfig().AsInt() == 1;
        if (useSwagger)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = appName, Version = appVersion });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Enter your Token JWT: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
            });
        }

        if (ConfigSettingEnum.StartWorker.GetConfig().AsInt() == 1)
        {
            //builder.Services.AddHostedService<AppHostedService>();
            builder.Services.AddTransient<IEventStorageRepository>(_ => new EventStorageRepository(
                ConfigSettingEnum.EventDatabaseConnectionString.GetConfig(),
                ConfigSettingEnum.EventDatabaseName.GetConfig(),
                ConfigSettingEnum.EventCollectionName.GetConfig()));

            string exChange = ConfigSettingEnum.RabbitMqExChange.GetConfig();
            string exChangeNotify = ConfigSettingEnum.RabbitMqExChangeNotifyListen.GetConfig();
            string exChangesTriggerConfig = ConfigSettingEnum.RabbitMqExChangeTriggerListen.GetConfig();
            if (exChange.Length > 0 || exChangeNotify.Length > 0 || exChangesTriggerConfig.Length > 0)
            {
                builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
            }
        }

        builder.Services.AddHostedService<AppInitHostedService>();
        if (registerServiceFunc != null)
        {
            registerServiceFunc(builder.Services);
        }

        var app = builder.Build();

        if (useSwagger)
        {
            //app.UseSwaggerAuthorized();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} {appVersion}");
                //c.RoutePrefix = "api";
            });
        }

        //if (ConfigSettingEnum.Https.GetConfig().AsInt() == 1)
        //{
        //    app.Use(async (ctx, next) =>
        //    {
        //        ctx.Request.Scheme = "https";
        //        if (ctx.Request.Path.HasValue && ctx.Request.Path.Value.Contains("/."))
        //        {
        //            ctx.Request.Path = "/error/404/";

        //        }

        //        await next();
        //    });
        //}

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseSerilogRequestLogging();
        app.UseRouting();
        if (corsConfig.Length > 0)
        {
            app.UseCors(Constant.CorsPolicy);
        }

        var forwardedHeaderOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto
        };
        forwardedHeaderOptions.KnownProxies.Clear();
        forwardedHeaderOptions.KnownNetworks.Clear();
        app.UseForwardedHeaders(forwardedHeaderOptions);
        app.UseResponseCompression();

        if (authenticationType > 0)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        if (registerRoutingUrl != null)
        {
            registerRoutingUrl(app);
        }

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapCodeFirstGrpcReflectionService();

        app.Run();
    }
}