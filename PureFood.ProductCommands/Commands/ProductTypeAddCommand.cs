using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands.Commands
{
    [ProtoContract]
    public record ProductTypeAddCommand : ProductBaseCommand
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public string ImageUrl { get; set; }
    }

    [ProtoContract]
    public record ProductTypeChangeCommand : ProductBaseCommand
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public string ImageUrl { get; set; }
    }
}
