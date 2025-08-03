using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public interface IMessageHandler
    {
        // void EventAdd(IEvent @event);
        // void EventAdd(IEnumerable<IEvent> @events);
        // IEvent?[] EventsGet(bool isTrigger = false);
        //public string QueueName { get; }
        public string WorkerGroup { get; }
    }
}
