using Microsoft.Extensions.Logging;
using PureFood.Common;
using PureFood.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public class RabbitMqConnection(
    IRabbitMqPersistentConnection persistentConnection,
    ILogger<RabbitMqConnection> logger)
    : RabbitMqBaseConnection(persistentConnection, logger, RabbitMqPrefetchCount), IRabbitMqConnection
    {
        private static readonly int RabbitMqPrefetchCount =
            ConfigSettingEnum.RabbitMqPrefetchCount.GetConfig().AsInt(10);
    }
}
