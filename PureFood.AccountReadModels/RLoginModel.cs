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
    public record RLoginModel
    {
        [ProtoMember(1)] public required string Token { get; set; }
        [ProtoMember(2)] public required int MinuteExpire { get; set; }
        [ProtoMember(3)] public required AccountLoginInfo User { get; set; }
        [ProtoMember(4)] public required string SessionId { get; set; }
        [ProtoMember(5)] public RUserDeviceMapping? UserDeviceMapping { get; set; } 
    }
}
