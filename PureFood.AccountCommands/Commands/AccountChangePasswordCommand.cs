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
    public record AccountChangePasswordCommand : AccountBaseCommand
    {
        [ProtoMember(1)] public string? OldPassword { get; set; }
        [ProtoMember(2)] public string? NewPassword { get; set; }
        [ProtoMember(3)] public string? ConfirmNewPassword { get; set; }
    }

    [ProtoContract]
    public record AccountForgotPasswordCommand : AccountBaseCommand
    {
        [ProtoMember(1)] public required string EmailOrMobile { get; set; }
        [ProtoMember(2)] public OtpTypeEnum? Type { get; set; }
    }

    [ProtoContract]
    public record class AccountResetPasswordCommand : AccountBaseCommand
    {
        [ProtoMember(1)] public string? ConfirmCode { get; set; }
        [ProtoMember(2)] public string? NewPassword { get; set; }
        [ProtoMember(3)] public string? ConfirmNewPassword { get; set; }
    }
}
