using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderReadModels
{
    [ProtoContract]
    public record ROderDetail : OrderBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string OrderId { get; set; }
        [ProtoMember(3)] public string ProductId { get; set; }
        [ProtoMember(4)] public string ProductName { get; set; }
        [ProtoMember(5)] public string ProductImage { get; set; }
        [ProtoMember(6)] public decimal Price { get; set; }
        [ProtoMember(7)] public int Quantity { get; set; }
        [ProtoMember(8)] public decimal TotalPrice { get; set; }
    }
}
