using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemDomains
{
    [Table("ConfigDomain_tbl")]
    public class ConfigDomain : BaseDomain
    {
        public new string Id { get; set; }
        public string Key { get; private set; }
        public string Description { get; private set; }
        public string Value { get; private set; }
        public StatusEnum Status { get; private set; }
        public ConfigTypeEnum Type { get; private set; }
    }
}
