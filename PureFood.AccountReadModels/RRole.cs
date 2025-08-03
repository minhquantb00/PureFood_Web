using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    public record RRole : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Name { get; set; }
        [ProtoMember(2)] public string Code { get; set; }   
    }
}
