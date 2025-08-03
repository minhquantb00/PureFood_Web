using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PureFood.BaseEvents.Event;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    public record RActionDefine : AccountBaseReadModel
    {
        [ProtoMember(1)] public string? Name { get; set; }
        [ProtoMember(2)] public string Group { get; set; }
        [ProtoMember(3)] public bool IsRoot { get; set; }
        [ProtoMember(4)] public StatusEnum Status { get; set; }
    }
}
