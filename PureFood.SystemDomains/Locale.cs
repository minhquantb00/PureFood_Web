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
    [Table("Locale_tbl")]
    public class Locale : BaseDomain
    {
        public new string Id { get; set; }
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public StatusEnum Status { get; set; }
        public int DisplayOrder { get; set; }
        public LocaleModuleEnum Module { get; set; }
    }
}
