using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using PureFood.AccountManager.Services;
using PureFood.AccountManager.Shared;
using PureFood.AccountRepository;
using PureFood.AccountRepositorySQLImplement;
using PureFood.Common;
using PureFood.Config;
using PureFood.ESRepositories;
using PureFood.HttpClientBase;

BaseProgram.Run(args, services =>
{
    services.AddTransient<IAccountService, AccountService>();
    services.AddTransient<IOTPLimitService, OTPLimitService>();
    //services.AddTransient<IAuthenticationService, AuthenticationService>();
;
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IUserRoleRepository, UserRoleRepository>();
    services.AddTransient<IForgotPasswordRepository, ForgotPasswordRepository>();

    services.AddSingleton<IESRepository>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<ESRepository>>();
        var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(nameof(HttpClientNameEnum.Default));
        IHttpClient vnnHttpClient = new PureFood.HttpClientBase.HttpClient(httpClient);
        string url = ConfigSettingEnum.EsUrl.GetConfig();
        string environment = ConfigSettingEnum.EnvironmentName.GetConfig();
        return new ESRepository(logger, url, vnnHttpClient, environment);
    });

    
    if (ConfigSettingEnum.StartWorker.GetConfig().AsInt() == 1)
    {
        services.AddHostedService<AccountHostedService>();
    }

    return services;
}, endpoints =>
{
    endpoints.MapGrpcService<AccountService>();
    endpoints.MapGrpcService<OTPLimitService>();
    endpoints.MapGrpcService<AuthenticationService>();
});