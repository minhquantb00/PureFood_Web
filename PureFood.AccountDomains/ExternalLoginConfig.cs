using PureFood.BaseDomains;
using PureFood.Common;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("ExternalLoginConfig_tbl")]
    public class ExternalLoginConfig : BaseDomain
    {

        public new string Id { get; set; }
        public StatusEnum Status { get; set; }
        public string Name { get; set; }
        public string? IconUrl { get; set; }
        public ExternalLoginProviderEnum? Option { get; set; }
        public List<ExternalLoginConfigItem>? Items { get; set; }
        public string? ItemsJson => Serialize.JsonSerializeObject(Items);
        public int Priority { get; set; }
    }
    [Table("ExternalLoginConfigItem_tbl")]
    public class ExternalLoginConfigItem : BaseDomain
    {
        public string Key { get; set; }
        public string? Value { get; set; }
    }
}
