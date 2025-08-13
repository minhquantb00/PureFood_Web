using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartReadModels
{
    [ProtoContract]
    public record RCartItem : CartBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2] public string CartId { get; set; }
        [ProtoMember(3))] public string ProductId { get; set; }
        [ProtoMember(4)] public int Quantity { get; set; }
    }
}
