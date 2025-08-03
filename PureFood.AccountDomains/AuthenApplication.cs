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
    [Table("AuthenApplication_tbl")]
    public class AuthenApplication : BaseDomain
    {
        public string Id { get; set; } //= Common.CommonUtility.GenerateGuid();
        public string Name { get; set; }
        public string? Info { get; set; }
        public string[]? ReturnUrls { get; set; }

        public string? ReturnUrlsJson
        {
            get => ReturnUrls == null ? null : Common.Serialize.JsonSerializeObject(ReturnUrls);
            set => ReturnUrls = string.IsNullOrWhiteSpace(value)
                ? null
                : Common.Serialize.JsonDeserializeObject<string[]>(value);
        }

        public string? ExternalLoginConfigIdsJson
        {
            get => ExternalLoginConfigIds == null ? null : Common.Serialize.JsonSerializeObject(ExternalLoginConfigIds);
            set => ExternalLoginConfigIds = string.IsNullOrWhiteSpace(value)
                ? null
                : Common.Serialize.JsonDeserializeObject<string[]>(value);
        }

        public string Secret { get; set; }
        public StatusEnum Status { get; set; }
        public string[]? ExternalLoginConfigIds { get; set; }
        public AuthenApplicationOptionEnum Options { get; set; }
        public int Interval { get; set; }
        public string? CheckUserExistUrl { get; set; }
        public int IsAutoRefreshToken { get; set; }
    }
}
