using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductReadModels
{
    [ProtoContract]
    public record RProduct : ProductBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string ProductTypeId { get; set; }
        [ProtoMember(3)] public string Name { get; set; }
        [ProtoMember(4)] public double Price { get; set; }
        [ProtoMember(5)] public string AvatarImage { get; set; }
        [ProtoMember(6)] public string Title { get; set; }
        [ProtoMember(7)] public int Discount { get; set; }
        [ProtoMember(8)] public double PriceDiscount { get; set; }
        [ProtoMember(9)] public int Quantity { get; set; }
        [ProtoMember(10)] public int NumberOfSales { get; set; }
        [ProtoMember(11)] public string Description { get; set; }
        [ProtoMember(12)] public string ShortDescription { get; set; }
        [ProtoMember(13)] public StatusEnum Status { get; set; }
    }
}
