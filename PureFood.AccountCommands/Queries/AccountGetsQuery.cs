using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountCommands.Queries
{
    [ProtoContract]
    public record AccountGetsQuery : AccountBaseCommand
    {
        [ProtoMember(1)] public string? KeyWord { get; set; }
        [ProtoMember(2)] public AccountStatusEnum? Status { get; set; }
        [ProtoMember(3)] public int PageIndex { get; set; }
        [ProtoMember(4)] public int PageSize { get; set; }
        [ProtoMember(5)] public AccountTypeEnum? AccountType { get; set; }
        [ProtoMember(6)] public string[]? DepartmentIds { get; set; }
        [ProtoMember(7)] public string[]? DepartmentIdsExclude { get; set; }
    }

    [ProtoContract]
    public record AccountGetByIdQuery : AccountBaseCommand
    {
        [ProtoMember(1)] public AccountTypeEnum AccountType { get; set; }
    }

    [ProtoContract]
    public record AccountGetByIdsQuery : AccountBaseCommand
    {
        [ProtoMember(1)] public string[] Ids { get; set; }
    }

    [ProtoContract]
    public record AccountGetByPhoneNumberQuery : AccountBaseCommand
    {
        [ProtoMember(1)] public required AccountTypeEnum AccountType { get; set; }
        [ProtoMember(2)] public required string PhoneNumber { get; set; }
    }
}
