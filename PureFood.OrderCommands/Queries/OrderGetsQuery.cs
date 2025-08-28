using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderCommands.Queries
{
    [ProtoContract]
    public record OrderGetsQuery : OrderBaseCommand
    {
        [ProtoMember(1)] public string? Keyword { get; set; }
        [ProtoMember(2)] public int PageIndex { get; set; }
        [ProtoMember(3)] public int PageSize { get; set; }
    }
    [ProtoContract]
    public record OrderGetByIdQuery : OrderBaseCommand
    {
        [ProtoMember(1)] public string? Id { get; set; }
    }
    [ProtoContract]
    public record OrderGetByIdsQuery : OrderBaseCommand
    {
        [ProtoMember(1)] public string[]? Ids { get; set; }
    }
}
