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
    [Table("WebsiteConfig_tbl")]
    public class WebsiteConfig : BaseDomain
    {
        public new string? Id { get; set; }
        public WebsiteConfigTypeEnum Type { get; set; }
        public string? ParentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FieldKey { get; set; }
        public string? FieldValue { get; set; }
        public string? FieldDefaultValue { get; set; }
        public WebsiteConfigFieldTypeEnum FieldType { get; set; }
        public string? TemplateId { get; set; }
        public string? WebsiteId { get; set; }
        public bool IsDisplay { get; set; }
        public StatusEnum Status { get; set; }
    }
}
