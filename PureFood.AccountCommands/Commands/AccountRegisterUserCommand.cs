using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountCommands.Commands
{
    [ProtoContract]
    public record AccountRegisterUserCommand : AccountBaseCommand
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string? EmailAddress { get; set; }
        [ProtoMember(3)] public string? NormalizedEmail { get; set; }
        [ProtoMember(4)] public string? FullName { get; set; }
        [ProtoMember(5)] public string? Password { get; set; }
        [ProtoMember(6)] public string? PhoneNumber { get; set; }
        [ProtoMember(7)] public bool IsPhoneNumberConfirmed { get; set; }
        [ProtoMember(8)] public bool IsTwoFactorEnabled { get; set; }
        [ProtoMember(9)] public bool IsEmailConfirmed { get; set; }
        [ProtoMember(10)] public string? AvatarUrl { get; set; }
        [ProtoMember(11)] public DateTime? Birthday { get; set; }
        [ProtoMember(12)] public GenderEnum? Gender { get; set; }
    }

    [ProtoContract]
    public record AccountLoginCommand : AccountBaseCommand
    {
        [ProtoMember(1)] public string? Email { get; set; }
        [ProtoMember(2)] public string? Password { get; set; }
        [ProtoMember(3)] public bool RememberMe { get; set; }
        [ProtoMember(4)] public string? ProviderKey { get; set; }
        [ProtoMember(5)] public required ExternalLoginProviderEnum LoginProvider { get; set; }
        [ProtoMember(8)] public required string ClientId { get; set; }
        [ProtoMember(9)] public string? DeviceLoginInfo { get; set; }
        [ProtoMember(10)] public string? IP { get; set; }
        [ProtoMember(11)] public string? UserId { get; set; }
        [ProtoMember(12)] public string? ExternalLoginConfigId { get; set; }
        [ProtoMember(13)] public string? ExternalLoginId { get; set; }
        [ProtoMember(14)] public string? ExternalToken { get; set; }
        [ProtoMember(15)] public string? ExternalRefreshToken { get; set; }
    }

    [ProtoContract]
    public record AccountChangeCommand : AccountBaseCommand
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string? EmailAddress { get; set; }
        [ProtoMember(3)] public string? NormalizedEmail { get; set; }
        [ProtoMember(4)] public string? FullName { get; set; }
        [ProtoMember(6)] public string? PhoneNumber { get; set; }
        [ProtoMember(7)] public bool IsPhoneNumberConfirmed { get; set; }
        [ProtoMember(8)] public bool IsTwoFactorEnabled { get; set; }
        [ProtoMember(9)] public bool IsEmailConfirmed { get; set; }
        [ProtoMember(10)] public string? AvatarUrl { get; set; }
        [ProtoMember(11)] public DateTime? Birthday { get; set; }
        [ProtoMember(12)] public GenderEnum? Gender { get; set; }
        [ProtoMember(13)] public AccountStatusEnum Status { get; set; }
        [ProtoMember(14)] public AccountTypeEnum Type { get; set; }
        [ProtoMember(15)] public OtpTypeEnum? OtpType { get; set; }
        [ProtoMember(16)] public OtpTypeEnum OtpTypeDefault { get; set; }
    }
}
