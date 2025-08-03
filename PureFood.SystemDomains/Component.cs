using PureFood.BaseDomains;
using PureFood.Common;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemDomains
{
    [Table("Component_tbl")]
    public class Component : BaseDomain
    {
        public new string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public string Thumbnail { get; set; }
        public ComponentTypeEnum Type { get; set; }
        public string WebsiteId { get; set; }
        public string GroupId { get; set; }
        public KeyValuePair<string, string>[]? JsObjects { get; set; }
        public KeyValuePair<string, string>[]? CssObjects { get; set; }
        public List<ComponentConditionObject> Conditions { get; set; }
        public int TotalPosition { get; set; }
        public string JsObjectsJson => Serialize.JsonSerializeObject(JsObjects);
        public string CssObjectsJson => Serialize.JsonSerializeObject(CssObjects);
        public string ConditionsJson => Serialize.JsonSerializeObject(Conditions);
        public NewsAdminSortTypeEnum SortType { get; set; }
        public NewsAdminSortFieldEnum SortField { get; set; }
        public bool IsDefault { get; set; }
        public int Priority { get; set; }
        public byte[]? HtmlData { get; set; }
        public byte[]? HtmlSourceData { get; set; }
        public string? Setting { get; set; }
        public string? EMagazineVersion { get; set; }
        public bool IsPublish { get; set; }
        public int VersionPublish { get; set; }
        public string? ParentId { get; set; }
        public string? TemplateId { get; set; }
        public ComponentDisplaySetting? DisplaySettings { get; set; }
        public string DisplaySettingsJson => Serialize.JsonSerializeObject(DisplaySettings);
    }

    public class ComponentDisplaySetting : BaseDomain
    {

        public ComponentDisplaySettingByDevice[]? DisplaySettings { get; private set; }
    }

    public class ComponentDisplaySettingByDevice
    {

        public StatusEnum Status { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string? Config { get; private set; }
        public PageDeviceTypeEnum? DeviceType { get; set; }
        public string? Html { get; set; }
    }
}
