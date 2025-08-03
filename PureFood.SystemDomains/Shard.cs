using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemDomains
{
    public class Shard : BaseDomain
    {
        public string Id { get; private set; }
        public string? Name { get; private set; }
        public string? ShardingGroupId { get; private set; }
        public ShardSetting? Setting { get; private set; }
        public StatusEnum Status { get; private set; }
        public string SettingJson => Common.Serialize.JsonSerializeObject(Setting);
    }
}
