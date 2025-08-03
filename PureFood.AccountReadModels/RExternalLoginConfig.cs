using ProtoBuf;
using PureFood.Common;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    public record RExternalLoginConfig : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public StatusEnum Status { get; set; }
        [ProtoMember(3)] public string Name { get; set; }
        [ProtoMember(4)] public string? IconUrl { get; set; }
        [ProtoMember(5)] public ExternalLoginProviderEnum? Option { get; set; }
        [ProtoMember(6)] public List<RExternalLoginConfigItem>? Items { get; set; }
        [ProtoMember(7)] public string? ItemsJson => Serialize.JsonSerializeObject(Items);
        [ProtoMember(8)] public int Priority { get; set; }
    }

    [ProtoContract]
    public record RExternalLoginConfigItem : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Key { get; set; }
        [ProtoMember(2)] public string? Value { get; set; }
    }
}
