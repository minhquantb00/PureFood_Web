using Microsoft.Extensions.DependencyInjection;
using PureFood.AccountManager.Shared;
using PureFood.CartManager.Shared;
using PureFood.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.GrpcClient
{
    public static partial class GrpcClientResolver
    {
        public static IServiceCollection CartManagerRegisterGrpcClient(this IServiceCollection services)
        {
            var accountManagerUrl = ConfigSettingEnum.CartManagerUrl.GetConfig();
            if (string.IsNullOrEmpty(accountManagerUrl)) return services;

            services.RegisterGrpcClientLoadBalancing<ICartService>(accountManagerUrl);
            services.RegisterGrpcClientLoadBalancing<ICartItemService>(accountManagerUrl);
            return services;
        }
    }
}
