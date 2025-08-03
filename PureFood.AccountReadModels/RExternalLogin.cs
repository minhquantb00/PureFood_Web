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
    public record RExternalLogin : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public ExternalLoginProviderEnum LoginProvider { get; set; }
        [ProtoMember(3)] public string? LoginProviderName { get; set; }
        [ProtoMember(4)] public string? ProviderKey { get; set; }
        [ProtoMember(5)] public string UserId { get; set; }
        [ProtoMember(6)] public string? Info { get; set; }
        [ProtoMember(7)] public string? Email { get; set; }
        [ProtoMember(8)] public string? PhoneNumber { get; set; }
        [ProtoMember(9)] public string? FullName { get; set; }
        [ProtoMember(10)] public string? Locale { get; set; }
        [ProtoMember(11)] public StatusEnum Status { get; set; }
        [ProtoMember(12)] public int? BirthYear { get; set; }
        [ProtoMember(13)] public int? BirthMonth { get; set; }
        [ProtoMember(14)] public int? BirthDay { get; set; }
        [ProtoMember(15)] public GenderEnum? Gender { get; set; }
    }
}
