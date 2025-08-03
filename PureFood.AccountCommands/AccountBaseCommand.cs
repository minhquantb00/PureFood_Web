using ProtoBuf;
using PureFood.AccountCommands.Commands;
using PureFood.AccountCommands.Queries;
using PureFood.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(AccountGetsQuery))]
    [ProtoInclude(300, typeof(AccountGetByIdQuery))]
    [ProtoInclude(400, typeof(AccountGetByIdsQuery))]
    [ProtoInclude(500, typeof(AccountGetByPhoneNumberQuery))]
    [ProtoInclude(600, typeof(AccountRegisterUserCommand))]
    [ProtoInclude(700, typeof(AccountLoginCommand))]
    [ProtoInclude(800, typeof(UserDeviceMappingAddCommand))]
    [ProtoInclude(900, typeof(AccountChangePasswordCommand))]
    [ProtoInclude(1000, typeof(AccountForgotPasswordCommand))]
    [ProtoInclude(1100, typeof(AccountResetPasswordCommand))]
    [ProtoInclude(1200, typeof(OtpLimitGetByKeywordQuery))]
    [ProtoInclude(1300, typeof(AccountChangeCommand))]
    public record AccountBaseCommand : BaseCommand
    {
        [ProtoMember(101)] public override string? ObjectId { get; set; }
        [ProtoMember(102)] public override string? ProcessUid { get; set; }
        [ProtoMember(103)] public override DateTime ProcessDate { get; set; }
        [ProtoMember(104)] public override string? LoginUid { get; set; }
    }
}
