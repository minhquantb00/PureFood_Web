using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountReadModels
{
    [ProtoContract]
    public record RRefreshToken : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Token { get; private set; }
        [ProtoMember(2)] public string UserId { get; private set; }
        [ProtoMember(3)] public DateTime ExpiredTime { get; private set; }
    }
}
