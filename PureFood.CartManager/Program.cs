using CartRepositorySQLImplement;
using PureFood.CartManager.Services;
using PureFood.CartManager.Shared;
using PureFood.CartRepository;
using PureFood.Common;
using PureFood.Config;
using PureFood.ESRepositories;
using PureFood.HttpClientBase;

BaseProgram.Run(args, services =>
{
    services.AddTransient<ICartRepository, CartRepository>();
    services.AddTransient<ICartItemRepository, CartItemRepository>();

    services.AddTransient<ICartService, CartService>();
    services.AddTransient<ICartItemService, CartItemService>();

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
        services.AddHostedService<CartHostedService>();
    }
    return services;
}, endpoints =>
{
    endpoints.MapGrpcService<CartService>();
    endpoints.MapGrpcService<CartItemService>();
});