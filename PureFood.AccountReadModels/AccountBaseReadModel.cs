using ProtoBuf;
using PureFood.BaseReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    [ProtoInclude(200, typeof(RAccountSetting))]
    [ProtoInclude(300, typeof(RActionDefine))]
    [ProtoInclude(400, typeof(RAddress))]
    [ProtoInclude(500, typeof(RAuthenApplication))]
    [ProtoInclude(600, typeof(RAuthenticatorSecretKey))]
    [ProtoInclude(700, typeof(RConfirmEmail))]
    [ProtoInclude(800, typeof(RDeviceCryptography))]
    [ProtoInclude(900, typeof(RExternalLogin))]
    [ProtoInclude(1000, typeof(RExternalLoginConfig))]
    [ProtoInclude(1100, typeof(RExternalLoginConfigItem))]
    [ProtoInclude(1200, typeof(RForgotPassword))]
    [ProtoInclude(1300, typeof(RRefreshToken))]
    [ProtoInclude(1400, typeof(RRole))]
    [ProtoInclude(1500, typeof(RUser))]
    [ProtoInclude(1600, typeof(RUserDevice))]
    public record AccountBaseReadModel : BaseReadModel
    {
        [ProtoMember(1)] public override long NumericalOrder { get; set; }
        [ProtoMember(2)] public override string Id { get; set; }
        [ProtoMember(3)] public override string? Code { get; set; }
        [ProtoMember(4)] public override required string CreatedUid { get; set; }
        [ProtoMember(5)] public override DateTime CreatedDate { get; set; }
        [ProtoMember(6)] public override DateTime CreatedDateUtc { get; set; }
        [ProtoMember(7)] public override required string UpdatedUid { get; set; }
        [ProtoMember(8)] public override DateTime UpdatedDate { get; set; }
        [ProtoMember(9)] public override DateTime UpdatedDateUtc { get; set; }
        [ProtoMember(10)] public override int Version { get; set; }
        [ProtoMember(11)] public override required string LoginUid { get; set; }
        [ProtoMember(12)] public override int TotalRow { get; set; }
        [ProtoMember(13)] public override string? ShardId { get; set; }
    }
}
