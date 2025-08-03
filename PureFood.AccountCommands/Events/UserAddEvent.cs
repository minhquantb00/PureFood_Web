using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountCommands.Events
{
    [ProtoContract]
    public record UserAddEvent : AccountBaseEvent
    {
        [ProtoMember(1)] public AccountTypeEnum AccountType { get; set; }
    }
    [ProtoContract]
    public record UserLoginEvent : AccountBaseEvent
    {
        [ProtoMember(1)] public string? DeviceInfo { get; set; }
        [ProtoMember(2)] public DateTime Date { get; set; }
        [ProtoMember(3)] public required string IP { get; set; }
    }
}
