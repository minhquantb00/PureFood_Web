using Microsoft.Extensions.DependencyInjection;
using PureFood.AccountManager.Shared;
using PureFood.Config;
using PureFood.EmailManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.GrpcClient
{

    public static partial class GrpcClientResolver
    {
        public static IServiceCollection EmailManagerRegisterGrpcClient(this IServiceCollection services)
        {
            var accountManagerUrl = ConfigSettingEnum.EmailManagerUrl.GetConfig();
            if (string.IsNullOrEmpty(accountManagerUrl)) return services;

            services.RegisterGrpcClientLoadBalancing<IEmailService>(accountManagerUrl);
            services.RegisterGrpcClientLoadBalancing<IEmailQueueService>(accountManagerUrl);
            return services;
        }
    }
}
