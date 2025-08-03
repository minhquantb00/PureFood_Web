using Microsoft.AspNetCore.Authentication;
using PureFood.AccountManager.Shared;
using PureFood.Common;
using PureFood.Config;
using PureFood.ESRepositories;
using PureFood.HttpClientBase;
using PureFood.ProductManager.Services;
using PureFood.ProductManager.Shared;
using PureFood.ProductRepository;
using PureFood.ProductRepositorySQLImplement;

BaseProgram.Run(args, services =>
{
    services.AddTransient<IProductService, ProductService>();
    services.AddTransient<IProductImageService, ProductImageService>();
    services.AddTransient<IProductTypeService, ProductTypeService>();

    services.AddTransient<IProductRepository, ProductRepository>();
    services.AddTransient<IProductImageRepository, ProductImageRepository>();
    services.AddTransient<IProductTypeRepository, ProductTypeRepository>();

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
        services.AddHostedService<ProductHostedService>();
    }

    return services;
}, endpoints =>
{
    endpoints.MapGrpcService<ProductService>();
    endpoints.MapGrpcService<ProductImageService>();
    endpoints.MapGrpcService<ProductTypeService>();
});