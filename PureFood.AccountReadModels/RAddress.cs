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
    public record RAddress : AccountBaseReadModel
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public StatusEnum Status { get; set; }
        [ProtoMember(3)] public string UserId { get; set; }
        [ProtoMember(4)] public string? CountryId { get; set; }
        [ProtoMember(5)] public int? ProvinceId { get; set; }
        [ProtoMember(6)] public int? DistrictId { get; set; }
        [ProtoMember(7)] public int? WardId { get; set; }
        [ProtoMember(8)] public int? StreetId { get; set; }
        [ProtoMember(9)] public string? Detail { get; set; }
        [ProtoMember(10)] public string? Description { get; set; }
        [ProtoMember(11)] public bool IsDefault { get; set; }
        [ProtoMember(12)] public AddressTypeEnum Type { get; set; }
        [ProtoMember(13)] public string? PhoneNumber { get; set; }
    }
}
