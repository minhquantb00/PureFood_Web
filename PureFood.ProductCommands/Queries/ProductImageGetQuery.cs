using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Queries
{
    [ProtoContract]
    public record ProductImageGetQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public required string Id { get; set; }
    }

    [ProtoContract]
    public record ProductImageGetsQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public string Keyword { get; set; }
        [ProtoMember(2)] public int PageSize { get; set; }
        [ProtoMember(3)] public int PageIndex { get; set; }
    }
}
