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
    public record RDeviceCryptography : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string? ServerRSAPrivateKey { get; set; }
        [ProtoMember(3)] public string? ServerRSAPublicKey { get; set; }
        [ProtoMember(4)] public string? DeviceRSAPrivateKey { get; set; }
        [ProtoMember(5)] public string? DeviceRSAPublicKey { get; set; }
        [ProtoMember(6)] public string? AuthenticatorSecretKey { get; set; }
        [ProtoMember(7)] public byte[]? AESIV { get; set; }
        [ProtoMember(8)] public DeviceCryptographyTypeEnum Type { get; set; }
        [ProtoMember(9)] public StatusEnum Status { get; set; }
    }
}
