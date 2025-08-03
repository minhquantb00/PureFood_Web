using PureFood.BaseEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public interface IEventProcessor
    {
        void Register();
        Dictionary<string, string> Handle(EventBusMessage payload);
        Task Start();
    }
}
