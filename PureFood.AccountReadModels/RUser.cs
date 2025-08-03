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
    public record RUser : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string? EmailAddress { get; set; }
        [ProtoMember(3)] public string? NormalizedEmail { get; set; }
        [ProtoMember(4)] public string? FullName { get; set; }
        [ProtoMember(5)] public string? Password { get; set; }
        [ProtoMember(6)] public string? PhoneNumber { get; set; }
        [ProtoMember(7)] public bool IsPhoneNumberConfirmed { get; set; }
        [ProtoMember(8)] public string? SecurityStamp { get; set; }
        [ProtoMember(9)] public bool IsTwoFactorEnabled { get; set; }
        [ProtoMember(10)] public bool IsEmailConfirmed { get; set; }
        [ProtoMember(11)] public string? PasswordSalt { get; set; }
        [ProtoMember(12)] public int AccessFailedCount { get; set; }
        [ProtoMember(13)] public string? AvatarUrl { get; set; }
        [ProtoMember(14)] public DateTime? Birthday { get; set; }
        [ProtoMember(15)] public GenderEnum? Gender { get; set; }
        [ProtoMember(16)] public AccountStatusEnum Status { get; set; }
        [ProtoMember(17)] public AccountTypeEnum Type { get; set; }
        [ProtoMember(18)] public OtpTypeEnum? OtpType { get; set; }
        [ProtoMember(19)] public OtpTypeEnum OtpTypeDefault { get; set; }
        [ProtoMember(20)] public string? ReferenceCode { get; private set; }
    }
}
