using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductReadModels
{
    [ProtoContract]
    public record RProductReview : ProductBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string ProductId { get; set; }
        [ProtoMember(3)] public string UserId { get; set; }
        [ProtoMember(4)] public int Rating { get; set; }
        [ProtoMember(5)] public string Comment { get; set; }
    }
}
