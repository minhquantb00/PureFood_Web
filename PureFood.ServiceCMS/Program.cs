using PureFood.BaseApplication.Middlewares;
using PureFood.Common;

BaseProgram.Run(args, services =>
{
    services.AddTransient<MultipartRequestUtility>();
    //services.AddTransient<AuthenFilterHandlerMiddleware>();
    return services;
}, endpoints => { /*endpoints.UseMiddleware<AuthenFilterHandlerMiddleware>();*/ });