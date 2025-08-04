using ProtoBuf;
using PureFood.BaseEvents;
using PureFood.ProductCommands.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(ProductAddEvent))]
    [ProtoInclude(300, typeof(ProductChangeEvent))]
    [ProtoInclude(400, typeof(ProductImageAddEvent))]
    [ProtoInclude(500, typeof(ProductImageChangeEvent))]
    [ProtoInclude(600, typeof(ProductTypeAddEvent))]
    [ProtoInclude(700, typeof(ProductTypeChangeEvent))]
    [ProtoInclude(800, typeof (ProductTypeChangeEvent))]
    [ProtoInclude(900, typeof(ProductTypeAddEvent))]
    public record ProductBaseEvent : Event
    {
        [ProtoMember(1)] public sealed override string EventId { get; set; } = Guid.CreateVersion7().ToString("N");
        [ProtoMember(2)] public override int Version { get; set; }
        [ProtoMember(3)] public override bool IsTrigger { get; set; }
        [ProtoMember(4)] public override string? ObjectId { get; set; }
        [ProtoMember(5)] public override string? ProcessUid { get; set; }
        [ProtoMember(6)] public override DateTime ProcessDate { get; set; }
        [ProtoMember(7)] public override string? LoginUid { get; set; }
        [ProtoMember(8)] public override int DelayTime { get; set; }
        public override EventTypeEnum EventType => EventTypeEnum.Product;
    }
}
