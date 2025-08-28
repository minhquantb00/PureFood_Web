using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderReadModels
{
    [ProtoContract]
    public record ROrder : OrderBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public OrderStatus OrderStatus { get; set; }
        [ProtoMember(3)] public string PaymentOrderId { get; set; }
        [ProtoMember(4)] public string UserId { get; set; }
        [ProtoMember(5)] public string Address { get; set; }
        [ProtoMember(6)] public string Email { get; set; }
        [ProtoMember(7)] public string FullName { get; set; }
        [ProtoMember(8)] public string PhoneNumber { get; set; }
        [ProtoMember(9)] public decimal ActualPrice { get; set; }
        [ProtoMember(10)] public decimal OriginalPrice { get; set; }
        [ProtoMember(11)] public string Note { get; set; }
        [ProtoMember(12)] public string Code { get; set; }
    }
}
