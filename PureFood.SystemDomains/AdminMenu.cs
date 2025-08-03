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
    [Table("AdminMenu_tbl")]
    public class AdminMenu : BaseDomain
    {
        public new string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public MenuTypeEnum Type { get; set; }
        public string Url { get; set; }
        public string ActionDefineId { get; set; }
        public MenuPosition PositionId { get; set; }
        public int Priority { get; set; }
        public StatusEnum Status { get; set; }
        public string ObjectId { get; set; }
        public string Condition { get; set; }
        public string CssClassIcon { get; set; }
        public bool IsDisplayPermission { get; set; }
        public SystemOptionEnum SystemOption { get; set; }
        public string PathBase { get; set; }
        public string BaseUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
