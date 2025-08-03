using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using PureFood.AccountManager.Shared;
using PureFood.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFood.GrpcClient;

namespace PureFood.GrpcClient
{
    public static partial class GrpcClientResolver
    {
        public static IServiceCollection AccountManagerRegisterGrpcClient(this IServiceCollection services)
        {
            var accountManagerUrl = ConfigSettingEnum.AccountManagerUrl.GetConfig();
            if (string.IsNullOrEmpty(accountManagerUrl)) return services;

            services.RegisterGrpcClientLoadBalancing<IAccountService>(accountManagerUrl);
            services.RegisterGrpcClientLoadBalancing<IOTPLimitService>(accountManagerUrl);
            //services.RegisterGrpcClientLoadBalancing<IAuthenticationService>(accountManagerUrl);
            return services;
        }
    }
}
