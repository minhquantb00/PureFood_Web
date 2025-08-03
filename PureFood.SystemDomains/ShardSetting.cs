using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemDomains
{
    [Table("ShardSetting_tbl")]
    public class ShardSetting : BaseDomain
    {
        public string? ESUrl { get; private set; }
        public string? ESIndexLastName { get; private set; }
        public string? DatabaseIp { get; private set; }
        public string? DatabaseName { get; private set; }
        public string? DatabaseUid { get; private set; }
        public string? DatabasePwd { get; private set; }
    }
}
