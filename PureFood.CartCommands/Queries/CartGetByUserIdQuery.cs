using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartCommands.Queries
{
    [ProtoContract]
    public record CartGetByUserIdQuery : CartBaseCommand
    {
        [ProtoMember(1)] public string UserId { get; set; }
    }
}
