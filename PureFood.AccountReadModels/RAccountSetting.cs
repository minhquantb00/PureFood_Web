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
    public record RAccountSetting : AccountBaseReadModel
    {
        [ProtoMember(1)] public string UserId { get; set; }
        [ProtoMember(2)] public AccountSettingEnum Type { get; set; }
        [ProtoMember(3)] public string? Description { get; set; }
        [ProtoMember(4)] public string? Value { get; set; }
        [ProtoMember(5)] public StatusEnum Status { get; set; }
    }
}
