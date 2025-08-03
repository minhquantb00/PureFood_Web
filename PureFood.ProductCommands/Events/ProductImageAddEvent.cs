using ProtoBuf;
using PureFood.BaseEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Events
{
    [ProtoContract]
    public record ProductImageAddEvent : ProductBaseEvent
    {
    }
    [ProtoContract]
    public record ProductImageChangeEvent : ProductBaseEvent
    {
    }
}
