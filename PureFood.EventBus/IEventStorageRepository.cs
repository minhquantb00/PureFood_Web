using PureFood.BaseEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EventBus
{
    public interface IEventStorageRepository
    {
        Task Add(EventBusMessage message, EventStatusEnum status, string exception);
    }
}
