using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartCommands.Commands
{
    [ProtoContract]
    public record CartAddCommand : CartBaseCommand
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string UserId { get; set; }
    }
}
