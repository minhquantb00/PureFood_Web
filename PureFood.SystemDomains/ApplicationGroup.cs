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
    [Table("ApplicationGroup_tbl")]
    public class ApplicationGroup : BaseDomain
    {
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public string? Description { get; set; }
        public string[]? WebsiteIdsObj { get; set; }
        public ApplicationGroupSetting SettingObj { get; set; }
        public string? Setting => Common.Serialize.JsonSerializeObject(SettingObj);
        public string? WebsiteIds => Common.Serialize.JsonSerializeObject(WebsiteIdsObj);
        
    }

    public class ApplicationGroupSetting
    {

        public string Name { get; set; }
        public int Port { get; set; }
        public string ServiceName { get; set; }
    }
}
