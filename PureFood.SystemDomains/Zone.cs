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
    [Table("Zone_tbl")]
    public class Zone : BaseDomain
    {
        public new string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public int Priority { get; set; }
        public string DealerId { get; set; }
        public int TotalPosition { get; set; }
        public string LockUserId { get; set; }
        public string LockSocketId { get; set; }
        public long LastOrder { get; set; }
        public string CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public List<ZonePosition>? ZonePositions { get; set; }
        public List<ZonePosition> ZonePositionsTimer { get; set; }
        public string ObjectId { get; set; }
        public ZoneObjectTypeEnum ObjectType { get; set; }
        public string MappingKey { get; set; }
        public string GroupId { get; set; }

        public MobilePageEnum MobilePage { get; set; }
        public long AutoSetPositionTime { get; set; }
        public int PRExpiredPosition { get; set; }
        public ZoneOptionEnum Options { get; set; }
    }
}
