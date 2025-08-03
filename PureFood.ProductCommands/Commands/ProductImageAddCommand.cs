using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Commands
{
    [ProtoContract]
    public record ProductImageAddCommand : ProductBaseCommand
    {
        [ProtoMember(1)] public string ProductId { get; set; }
        [ProtoMember(2)] public string ImageUrl { get; set; }
        [ProtoMember(3)] public string Title { get; set; }
        [ProtoMember(4)] public string Id { get; set; }
    }

    [ProtoContract]
    public record ProductImageChangeCommand : ProductBaseCommand
    {
        [ProtoMember(1)] public string ProductId { get; set; }
        [ProtoMember(2)] public string ImageUrl { get; set; }
        [ProtoMember(3)] public string Title { get; set; }
        [ProtoMember(4)] public string Id { get; set; }
    }

    [ProtoContract]
    public record ProductImageDeleteCommand : ProductBaseCommand
    {
        [ProtoMember(1)] public required string Id { get; set; }
    }
}
