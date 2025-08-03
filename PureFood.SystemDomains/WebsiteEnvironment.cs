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
    [Table("WebsiteEnvironment_tbl")]
    public class WebsiteEnvironment : BaseDomain
    {
        public new string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public StatusEnum Status { get; set; }
        public string CompanyId { get; set; }
        public string WebsiteId { get; set; }
        public string AppName { get; set; }
    }
}
