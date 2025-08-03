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
    public record RAuthenApplication : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public string? Info { get; set; }
        [ProtoMember(4)] public string[]? ReturnUrls { get; set; }
        [ProtoMember(5)] public string? ReturnUrlsJson { get; set; }
        [ProtoMember(6)] public string? ExternalLoginConfigIdsJson { get; set; }
        [ProtoMember(7)] public string Secret { get; set; }
        [ProtoMember(8)] public StatusEnum Status { get; set; }
        [ProtoMember(9)] public string[]? ExternalLoginConfigIds { get; set; }
        [ProtoMember(10)] public AuthenApplicationOptionEnum Options { get; set; }
        [ProtoMember(11)] public int Interval { get; set; }
        [ProtoMember(12)] public string? CheckUserExistUrl { get; set; }
        [ProtoMember(13)] public int IsAutoRefreshToken { get; set; }
    }
}
