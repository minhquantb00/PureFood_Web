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
    [Table("ShardGroup_tbl")]
    public class ShardGroup : BaseDomain
    {
        public new string Id { get; private set; }
        public string? Name { get; private set; }
        public ShardingTypeEnum Type { get; private set; }
        public int TotalShard { get; private set; }
        public StatusEnum Status { get; private set; }
        public List<Shard>? Items { get; private set; }
    }
}
