using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        Task<bool> TryConnect();

        Task<IChannel> CreateChannel();
        string GetHosts();
    }
}
