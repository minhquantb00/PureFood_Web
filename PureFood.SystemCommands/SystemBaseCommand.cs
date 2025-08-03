using ProtoBuf;
using PureFood.BaseCommands;
using PureFood.SystemCommands.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(GetNextCodeQuery))]
    [ProtoInclude(201, typeof(GetNextMultipleCodeQuery))]
    public record SystemBaseCommand : BaseCommand
    {
        [ProtoMember(1)] public sealed override string ObjectId { get; set; }
        [ProtoMember(2)] public sealed override string ProcessUid { get; set; }
        [ProtoMember(3)] public sealed override DateTime ProcessDate { get; set; }
        [ProtoMember(4)] public sealed override string LoginUid { get; set; }
        [ProtoMember(6)] public string? DealerId { get; set; }
        [ProtoMember(5)] public bool IsCache { get; set; }
    }
}
