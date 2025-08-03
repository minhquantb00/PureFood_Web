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
    [Table("ZonePosition_tbl")]
    public class ZonePosition : BaseDomain
    {
        public new string Id { get; set; }
        public ZonePositionType Type { get; set; }
        public string ObjectId { get; set; }
        public string ZoneId { get; set; }
        public ActiveStatusEnum Status { get; set; }
        public int Position { get; set; }
        public int PositionOld { get; set; }
        public int Priority { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long Order { get; set; }
        public bool IsPin { get; set; }
        public bool PinFinish { get; set; }
        public bool PinStart { get; set; }
        public bool IsAuto { get; set; }
        public DateTime? DisplayTime { get; set; }
        public DateTime? OffTime { get; set; }

        public ZonePositionDisplayTypeEnum DisplayType { get; set; }
        public bool IsCurrentPinFinish { get; set; }
    }
}
