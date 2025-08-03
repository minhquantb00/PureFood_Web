using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public class PureFoodObjectPool(IRabbitMqPersistentConnection persistentConnection, ILogger logger)
    {
        private readonly ConcurrentBag<IChannel> _objects = [];

        // private readonly Func<Task<IChannel>> _objectGenerator =
        //     objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));

        public async Task<IChannel?> Get()
        {
            if (_objects.TryTake(out var item))
            {
                if (!item.IsOpen || item.IsClosed)
                {
                    try
                    {
                        await item.DisposeAsync();
                    }
                    catch (Exception e)
                    {
                        logger.LogWarning(e, e.Message);
                    }

                    return await persistentConnection.CreateChannel();
                }
                return item;
            }

            return await persistentConnection.CreateChannel();
        }

        public void Return(IChannel? item)
        {
            if (item != null)
            {
                if (!item.IsOpen || item.IsClosed)
                {
                    return;
                }

                _objects.Add(item);
            }
        }
    }
}
