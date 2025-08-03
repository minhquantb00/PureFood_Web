using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    public record RAuthenticatorSecretKey : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public OtpTypeEnum Type { get; set; }
        [ProtoMember(3)] public string Key { get; set; }
        [ProtoMember(4)] public string Info { get; set; }
        [ProtoMember(5)] public bool IsDefault { get; set; }
        [ProtoMember(6)] public StatusEnum Status { get; set; }
    }
}
