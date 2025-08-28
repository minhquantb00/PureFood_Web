using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderCommands.Events
{
    [ProtoContract]
    public record OrderAddEvent : OrderBaseEvent
    {
    }

    [ProtoContract]
    public record OrderChangeEvent : OrderBaseEvent
    {
    }
}
