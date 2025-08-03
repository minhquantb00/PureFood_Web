using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("Address_tbl")]
    public class Address : BaseDomain
    {
        public new string Id { get; private set; }
        public StatusEnum Status { get; private set; }
        public string UserId { get; private set; }
        public string? CountryId { get; private set; }
        public int? ProvinceId { get; private set; }
        public int? DistrictId { get; private set; }
        public int? WardId { get; private set; }
        public int? StreetId { get; private set; }
        public string? Detail { get; private set; }
        public string? Description { get; private set; }
        public bool IsDefault { get; private set; }
        public AddressTypeEnum Type { get; private set; }
        public string? PhoneNumber { get; private set; }
    }
}
