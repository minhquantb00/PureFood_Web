using Microsoft.Extensions.DependencyInjection;
using PureFood.AccountManager.Shared;
using PureFood.Config;
using PureFood.ProductManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.GrpcClient
{
    public static partial class GrpcClientResolver
    {
        public static IServiceCollection ProductManagerRegisterGrpcClient(this IServiceCollection services)
        {
            var accountManagerUrl = ConfigSettingEnum.ProductManagerUrl.GetConfig();
            if (string.IsNullOrEmpty(accountManagerUrl)) return services;

            services.RegisterGrpcClientLoadBalancing<IProductService>(accountManagerUrl);
            services.RegisterGrpcClientLoadBalancing<IProductImageService>(accountManagerUrl);
            services.RegisterGrpcClientLoadBalancing<IProductTypeService>(accountManagerUrl);
            services.RegisterGrpcClientLoadBalancing<IProductReviewService>(accountManagerUrl);
            return services;
        }
    }
}
