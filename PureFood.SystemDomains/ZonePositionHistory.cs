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
    [Table("ZonePositionHistory_tbl")]
    public class ZonePositionHistory : BaseDomain
    {
        public int Position { get; set; }
        public ZonePositionDisplayTypeEnum DisplayType { get; set; }
        public string ZoneId { get; set; }
        //public RZonePosition ZonePositionOld { get; set; }
        //public RZonePosition ZonePositionNew { get; set; }
        public string ChangeId { get; set; }
        public string ObjectIdOld { get; set; }
        public string ObjectIdNew { get; set; }
        public ZonePositionActionTypeEnum ActionType { get; set; }
        public bool IsAuto { get; set; }
        public ZonePositionType TypeOld { get; set; }
        public ZonePositionType TypeNew { get; set; }
        public bool? IsPin { get; set; }
    }
}
