using PureFood.Common;
using PureFood.Config;
using PureFood.ESRepositories;
using PureFood.HttpClientBase;
using PureFood.OrderManager.Services;
using PureFood.OrderManager.Shared;
using PureFood.OrderRepository;
using PureFood.OrderRepositorySQLImplement;

BaseProgram.Run(args, services =>
{
    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<IOrderRepository, OrderRepository>();
    services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
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
        services.AddHostedService<OrderHostedService>();
    }
    return services;
}, endpoints =>
{
    endpoints.MapGrpcService<OrderService>();
});