using CartRepositorySQLImplement;
using PureFood.CartManager.Services;
using PureFood.CartManager.Shared;
using PureFood.CartRepository;

BaseProgram.Run(args, services =>
{
    services.AddTransient<ICartRepository, CartRepository>();
    services.AddTransient<ICartItemRepository, CartItemRepository>();

    services.AddTransient<ICartService, CartService>();
    services.AddTransient<ICartItemService, CartItemService>();
    return services;
}, endpoints =>
{
    endpoints.MapGrpcService<CartService>();
    endpoints.MapGrpcService<CartItemService>();
});