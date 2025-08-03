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
    [Table("PageComponentMapping_tbl")]
    public class PageComponentMapping : BaseDomain
    {
        public new string Id { get; set; }
        public string? PageId { get; set; }
        public string? ComponentId { get; set; }
        public string? WebsiteId { get; set; }
        public int Level { get; set; }
        public PageDeviceTypeEnum PageDeviceType { get; set; }
        public bool? IsPublish { get; set; }
        public string? TemplateId { get; set; }
        public bool? IsTemplate { get; set; }
        public string[]? ComponentIdsObj { get; set; }
        public string ComponentIds => Common.Serialize.JsonSerializeObject(ComponentIdsObj);
        public StatusEnum Status { get; set; }
    }
}
