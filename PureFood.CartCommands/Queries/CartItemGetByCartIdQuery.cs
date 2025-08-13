using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartCommands.Queries
{
    [ProtoContract]
    public record CartItemGetByCartIdQuery : CartBaseCommand
    {
        [ProtoMember(1)] public string CartId { get; set; }
    }
}
