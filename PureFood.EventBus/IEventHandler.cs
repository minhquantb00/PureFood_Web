using PureFood.BaseEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public interface IEventHandler<in TI> : IMessageHandler where TI : IEvent
    {
        Task Handle(TI message, string topic);
    }
}
