using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Queries
{
    [ProtoContract]
    public record ProductReviewGetQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public string Id { get; set; }
    }

    [ProtoContract]
    public record ProductReviewGetByIdsQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public string[] Ids { get; set; }
    }

    [ProtoContract]
    public record ProductReviewGetsQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public string Keyword { get; set; }
        [ProtoMember(2)] public int PageIndex { get; set; }
        [ProtoMember(3)] public int PageSize { get; set; }
    }
}
