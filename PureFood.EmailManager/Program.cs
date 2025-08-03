using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using PureFood.AccountManager.Shared;
using PureFood.Common;
using PureFood.Config;
using PureFood.EmailManager.Services;
using PureFood.EmailManager.Shared;
using PureFood.EmailReadModels;
using PureFood.HttpClientBase;

BaseProgram.Run(args, services =>
{

    var tempProvider = services.BuildServiceProvider();
    var configuration = tempProvider.GetRequiredService<IConfiguration>();

    var emailConfigSection = configuration.GetSection("EmailConfiguration");
    services.Configure<EmailConfiguration>(emailConfigSection);
    services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfiguration>>().Value);
    services.AddTransient<IEmailQueueService, EmailQueueService>();
    services.AddTransient<IEmailService, EmailService>();

    if (ConfigSettingEnum.StartWorker.GetConfig().AsInt() == 1)
    {
        services.AddHostedService<EmailHostedService>();
    }

    return services;
}, endpoints =>
{
    endpoints.MapGrpcService<EmailQueueService>();
    endpoints.MapGrpcService<EmailService>();
});