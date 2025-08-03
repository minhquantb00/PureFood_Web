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
    public record RUserDevice : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string UserDeviceMappingId { get; set; }
        [ProtoMember(3)] public string? Token { get; set; }
        [ProtoMember(4)] public string? DeviceLoginInfo { get; set; }
        [ProtoMember(5)] public string? IP { get; set; }
        [ProtoMember(6)] public string? ParentId { get; set; }
        [ProtoMember(7)] public DateTime ExpireDate { get; set; }
        [ProtoMember(8)] public string ClientId { get; set; }
        [ProtoMember(9)] public bool RememberMe { get; set; }
        [ProtoMember(10)] public bool Trusted { get; set; }
        [ProtoMember(11)] public StatusEnum Status { get; set; }
        [ProtoMember(12)] public LoginTypeEnum LoginType { get; set; }
        [ProtoMember(13)] public string? FCMToken { get; set; }
        [ProtoMember(14)] public DateTime? OtpVerifyTime { get; set; }
        [ProtoMember(15)] public DateTime? OtpCMSVerifyTime { get; set; }
        [ProtoMember(16)] public List<RDeviceCryptography>? Cryptographies { get; private set; }
        [ProtoMember(17)] public string Cryptography { get; set; }
    }
}
