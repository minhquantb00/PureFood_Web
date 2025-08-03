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
    [Table("Page_tbl")]
    public class Page : BaseDomain
    {
        public new string Id { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? Thumbnail { get; private set; }
        public byte[]? HtmlData { get; private set; }
        public byte[]? HtmlMobileData { get; private set; }
        public byte[]? HtmlTabletData { get; private set; }
        public byte[]? HtmlAMPData { get; private set; }

        public int Priority { get; private set; }
        public StatusEnum Status { get; private set; }
        public PageTypeEnum Type { get; private set; }
        public KeyValuePair<string, string>[]? Parameters { get; private set; }
        public string ParametersJson => Serialize.JsonSerializeObject(Parameters);
        public string? WebsiteId { get; private set; }
        public string? TemplateId { get; private set; }
        public bool IsDefault { get; private set; }
        public KeyValuePair<string, string>[]? JsObjects { get; private set; }
        public KeyValuePair<string, string>[]? CssObjects { get; private set; }

        public KeyValuePair<string, string>[]? JsObjectsMobile { get; private set; }
        public KeyValuePair<string, string>[]? CssObjectsMobile { get; private set; }
        public KeyValuePair<string, string>[]? JsObjectsTablet { get; private set; }
        public KeyValuePair<string, string>[]? CssObjectsTablet { get; private set; }
        public KeyValuePair<string, string>[]? JsObjectsAMP { get; private set; }
        public KeyValuePair<string, string>[]? CssObjectsAMP { get; private set; }
        public List<ComponentConditionObject> Conditions { get; private set; }
        public string JsObjectsJson => Serialize.JsonSerializeObject(JsObjects);
        public string CssObjectsJson => Serialize.JsonSerializeObject(CssObjects);
        public string JsObjectsJsonMobile => Serialize.JsonSerializeObject(JsObjectsMobile);
        public string CssObjectsJsonMobile => Serialize.JsonSerializeObject(CssObjectsMobile);
        public string JsObjectsJsonTablet => Serialize.JsonSerializeObject(JsObjectsTablet);
        public string CssObjectsJsonTablet => Serialize.JsonSerializeObject(CssObjectsTablet);
        public string JsObjectsJsonAMP => Serialize.JsonSerializeObject(JsObjectsAMP);
        public string CssObjectsJsonAMP => Serialize.JsonSerializeObject(CssObjectsAMP);
        public string ConditionsJson => Serialize.JsonSerializeObject(Conditions);
        public string? ParentId { get; private set; }
        public string? ObjectId { get; private set; }
        public UrlPath[]? UrlPathsObject { get; private set; }
        public string UrlPaths => Serialize.JsonSerializeObject(UrlPathsObject);
        public List<FileByDevice>? CssBundleByDevice { get; private set; }
        public List<FileByDevice>? JsBundleByDevice { get; private set; }
        public string CssBundle => Serialize.JsonSerializeObject(CssBundleByDevice);
        public string JsBundle => Serialize.JsonSerializeObject(JsBundleByDevice);
        public byte[]? CssBundleData { get; private set; }
        public byte[]? JsBundleData { get; private set; }
        public int VersionCache { get; private set; }
        public bool IsActive { get; private set; }
        public string? QueryStrings { get; private set; }
        public long? CacheTime { get; private set; }
        public long? CacheTimeValid { get; private set; }
        public PageSettings Settings { get; private set; }
        public string? ModelBindingName { get; private set; }
        public bool IsPublish { get; private set; }
        public int VersionPublish { get; private set; }
        public bool? IsTemplate { get; set; }
        public string? EditSetting { get; set; }
    }

    public class ComponentConditionObject
    {
        //public ComponentConditionObject(RComponentConditionObject conditionObject)
        //{
        //    Key = conditionObject.Key.AsEmpty();
        //    Values = conditionObject.Values;
        //    Type = conditionObject.Type;
        //}

        //public ComponentConditionObject(ComponentConditionAddCommand command)
        //{
        //    Key = command.Key.AsEmpty();
        //    Values = command.Values;
        //    Type = command.Type;
        //}

        public string Key { get; set; }
        public string[]? Values { get; set; }
        public ComponentConditionTypeEnum Type { get; set; }
    }
    public class UrlPath
    {
        //public UrlPath(RUrlPath urlPath)
        //{
        //    Path = urlPath.Path;
        //    IsMatchRule = urlPath.IsMatchRule;
        //}

        public UrlPath(string path)
        {
            Path = path;
            IsMatchRule = path.Contains("{") && path.Contains("}");
        }

        public string Path { get; set; }
        public bool IsMatchRule { get; set; }
    }

    public class FileByDevice
    {
        //public FileByDevice(RFileByDevice fileByDevice)
        //{
        //    DeviceType = fileByDevice.DeviceType;
        //    Content = fileByDevice.Content;
        //    Url = fileByDevice.Url;
        //}

        public FileByDevice(PageDeviceTypeEnum deviceType, string content)
        {
            DeviceType = deviceType;
            Content = content;
        }

        public void SetUrl(string url)
        {
            if (url?.Length > 0)
            {
                Url = url;
            }
        }

        public void RemoveContent()
        {
            Content = null;
        }

        public PageDeviceTypeEnum DeviceType { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
    }


    public class PagePublish : BaseDomain
    {
        public PagePublish(Page page)
        {
            CreatedDate = UpdatedDate = page.UpdatedDate;
            CreatedDateUtc = UpdatedDateUtc = page.UpdatedDateUtc;
            CreatedUid = UpdatedUid = page.UpdatedUid;
            Id = page.Id;
            Name = page.Name;
            Description = page.Description;
            Thumbnail = page.Thumbnail;
            JsObjectsJson = page.JsObjectsJson;
            CssObjectsJson = page.CssObjectsJson;
            Priority = page.Priority;
            Status = page.Status;
            Type = page.Type;
            ParametersJson = page.ParametersJson;
            WebsiteId = page.WebsiteId;
            TemplateId = page.TemplateId;
            IsDefault = page.IsDefault;
            ParentId = page.ParentId;
            ObjectId = page.ObjectId;
            UrlPaths = page.UrlPaths;
            CssBundle = page.CssBundle;
            JsBundle = page.JsBundle;
            HtmlData = page.HtmlData;
            CssBundleData = page.CssBundleData;
            JsBundleData = page.JsBundleData;
            Version = page.Version;
            VersionCache = page.VersionCache;
            IsActive = page.IsActive;
            QueryStrings = page.QueryStrings;
            CacheTime = page.CacheTime;
            CacheTimeValid = page.CacheTimeValid;
            Settings = page.Settings;
            ModelBindingName = page.ModelBindingName;
            IsPublish = page.IsPublish;
            VersionPublish = page.VersionPublish;
        }

        public new string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Thumbnail { get; private set; }
        public string JsObjectsJson { get; private set; }
        public string CssObjectsJson { get; private set; }
        public int Priority { get; private set; }
        public StatusEnum Status { get; private set; }
        public PageTypeEnum Type { get; private set; }
        public string ParametersJson { get; private set; }
        public string WebsiteId { get; private set; }
        public string TemplateId { get; private set; }
        public bool IsDefault { get; private set; }
        public string ParentId { get; private set; }
        public string ObjectId { get; private set; }
        public string UrlPaths { get; private set; }
        public string CssBundle { get; private set; }
        public string JsBundle { get; private set; }
        public byte[]? HtmlData { get; private set; }
        public byte[]? CssBundleData { get; private set; }
        public byte[]? JsBundleData { get; private set; }
        public new int Version { get; private set; }
        public int VersionCache { get; private set; }
        public bool IsActive { get; private set; }
        public string QueryStrings { get; private set; }
        public long? CacheTime { get; private set; }
        public long? CacheTimeValid { get; private set; }
        public PageSettings Settings { get; private set; }
        public string? ModelBindingName { get; private set; }
        public bool IsPublish { get; private set; }
        public int VersionPublish { get; private set; }

    }

    public class PagePublishHistory : PagePublish
    {
        public PagePublishHistory(Page page) : base(page)
        {
            Id = Common.CommonUtility.GenerateGuid();
            PageId = page.Id;
        }

        public new string Id { get; set; }
        public string PageId { get; set; }

    }
}
