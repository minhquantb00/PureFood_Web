using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductReadModels
{
    [ProtoContract]
    public record RProductType : ProductBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public string ImageUrl { get; set; }
    }
}
