using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountCommands.Queries
{
    [ProtoContract]
    public record OtpLimitGetByKeywordQuery : AccountBaseCommand
    {
        [ProtoMember(1)] public string? Keyword { get; set; }
        [ProtoMember(2)] public int PageSize { get; set; }
        [ProtoMember(3)] public int PageIndex { get; set; }
    }
}
