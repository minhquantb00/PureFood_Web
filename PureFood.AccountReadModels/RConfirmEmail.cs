using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    public record RConfirmEmail : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string ConfirmCode { get; set; }
        [ProtoMember(3)] public string UserId { get; set; }
        [ProtoMember(4)] public DateTime ExpiredTime { get; set; }
        [ProtoMember(5)] public bool IsConfirmed { get; set; }
    }
}
