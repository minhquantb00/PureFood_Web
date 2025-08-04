using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Events
{
    [ProtoContract]
    public record ProductReviewAddEvent : ProductBaseEvent
    {
    }

    [ProtoContract]
    public record ProductReviewChangeEvent : ProductBaseEvent
    {
    }
}
