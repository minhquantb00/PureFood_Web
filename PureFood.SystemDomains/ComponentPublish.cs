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
    [Table("ComponentPublish_tbl")]
    public class ComponentPublish : BaseDomain
    {
        public ComponentPublish(Component component)
        {
            CreatedDate = UpdatedDate = component.UpdatedDate;
            CreatedDateUtc = UpdatedDateUtc = component.UpdatedDateUtc;
            CreatedUid = UpdatedUid = component.UpdatedUid;
            Id = component.Id;
            Name = component.Name;
            Description = component.Description;
            Status = component.Status;
            Thumbnail = component.Thumbnail;
            Type = component.Type;
            WebsiteId = component.WebsiteId;
            GroupId = component.GroupId;
            TotalPosition = component.TotalPosition;
            JsObjectsJson = component.JsObjectsJson;
            CssObjectsJson = component.CssObjectsJson;
            ConditionsJson = component.ConditionsJson;
            SortType = component.SortType;
            SortField = component.SortField;
            IsDefault = component.IsDefault;
            Priority = component.Priority;
            HtmlData = component.HtmlData;
            Setting = component.Setting;
            EMagazineVersion = component.EMagazineVersion;
            IsPublish = component.IsPublish;
            VersionPublish = component.VersionPublish;
            Version = component.Version;
            ParentId = component.ParentId;
            TemplateId = component.TemplateId;
            DisplaySettings = component.DisplaySettings;
        }

        public new string Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StatusEnum Status { get; private set; }
        public string Thumbnail { get; private set; }
        public ComponentTypeEnum Type { get; private set; }
        public string WebsiteId { get; private set; }
        public string GroupId { get; private set; }
        public int TotalPosition { get; private set; }
        public string JsObjectsJson { get; private set; }
        public string CssObjectsJson { get; private set; }
        public string ConditionsJson { get; private set; }
        public NewsAdminSortTypeEnum SortType { get; private set; }
        public NewsAdminSortFieldEnum SortField { get; private set; }
        public bool IsDefault { get; private set; }
        public int Priority { get; private set; }
        public byte[]? HtmlData { get; private set; }
        public string? Setting { get; private set; }
        public string? EMagazineVersion { get; private set; }
        public bool IsPublish { get; private set; }
        public int VersionPublish { get; private set; }
        public string? ParentId { get; private set; }
        public string? TemplateId { get; private set; }
        public ComponentDisplaySetting? DisplaySettings { get; private set; }
        public string DisplaySettingsJson => Common.Serialize.JsonSerializeObject(DisplaySettings);
    }

    public class ComponentPublishHistory : ComponentPublish
    {
        public ComponentPublishHistory(Component component) : base(component)
        {
            Id = Common.CommonUtility.GenerateGuid();
            ComponentId = component.Id;
        }

        public new string Id { get; private set; }
        public string ComponentId { get; private set; }
    }
}
