using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Queries
{
    [ProtoContract]
    public record ProductTypeGetQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public required string Id { get; set; }
    }
    [ProtoContract]
    public record ProductTypeGetByIdsQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public required string[] Ids { get; set; }
    }

    [ProtoContract]
    public record ProductTypeGetsQuery : ProductBaseCommand
    {
        [ProtoMember(1)] public string Keyword { get; set; }
        [ProtoMember(2)] public int PageIndex { get; set; }
        [ProtoMember(3)] public int PageSize { get; set; }
    }
}
