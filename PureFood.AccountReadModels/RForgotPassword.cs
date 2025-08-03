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
    public record RForgotPassword : AccountBaseReadModel
    {
        [ProtoMember(1)] public string UserId { get; set; }
        [ProtoMember(2)] public string EmailOrMobile { get; set; }
        [ProtoMember(3)] public ForgotPasswordStatusEnum Status { get; set; }
        [ProtoMember(4)] public DateTime Expired { get; set; }
        [ProtoMember(5)] public int ExpiresTime { get; set; }
        [ProtoMember(6)] public RAuthenticatorSecretKey? AuthenticatorSecretKeyObj { get; set; }
        [ProtoMember(7)] public string? AuthenticatorSecretKey { get; set; }
        [ProtoMember(8)] public bool IsExpired => Expired < DateTime.Now;
        [ProtoMember(9)] public string? VerificationCode { get; set; }
        [ProtoMember(10)] public string? Id { get; set; }
    }
}
