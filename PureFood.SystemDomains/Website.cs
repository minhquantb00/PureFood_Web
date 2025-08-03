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
    [Table("Website_tbl")]
    public class Website : BaseDomain
    {
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public string CompanyId { get; set; }
        public string WebsiteCode { get; set; }
        public string Domain { get; set; }
        public HttpProtocolEnum Protocol { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DepartmentId { get; set; }
        public WebsiteSetting[]? SettingsObject { get; set; }
        public StatusAudioEnum Audio { get; set; }
        public string? ServerGroup { get; set; }
        public WebsiteDomain[]? Domains { get; set; }
        public List<WebsiteEnvironment>? Environments { get; set; }
        public string? TemplateId { get; set; }

        public string Settings => SettingsObject != null
            ? Common.Serialize.JsonSerializeObject(SettingsObject)
            : string.Empty;

        public string FullDomain => $"{Protocol}://{Domain}";
    }


    [Table("Website_tbl")]
    public class WebsitePublish : Website
    {
        //public WebsitePublish(RWebsitePublish website, RWebsiteDomain[] websiteDomains, RWebsiteEnvironment[] environments)
        //    : base(website)
        //{
        //    VersionPublish = website.VersionPublish;
        //    PublishInfo = website.PublishInfo;
        //    Domains = websiteDomains?.Select(p => new WebsiteDomain(p)).ToArray();
        //    Environments = environments?.Select(p => new WebsiteEnvironment(p)).ToList();
        //}

        //public (string FilePath, string Content)[] Publish(WebsitePublishCommand command)
        //{
        //    if (Domains is not { Length: > 0 })
        //    {
        //        throw new TYTException("Website domain is not config");
        //    }

        //    if (Environments is not { Count: > 0 })
        //    {
        //        throw new TYTException("Website environment is not config");
        //    }

        //    Version++;
        //    VersionPublish = Version;
        //    PublishInfo = Common.Serialize.JsonSerializeObject(this);
        //    Changed(command);
        //    return GetFileEnvironments();
        //}

        private (string FilePath, string Content)[] GetFileEnvironments()
        {
            List<(string FilePath, string Content)> files = new List<(string FilePath, string Content)>();
            if (Environments?.Count > 0)
            {
                foreach (var environment in Environments)
                {
                    files.Add((environment.Name, environment.Value));
                }
            }

            return files.ToArray();
        }

        public int VersionPublish { get; set; }
        public string PublishInfo { get; set; }
    }


    public class WebsiteSetting
    {
        //public WebsiteSetting(RWebsiteSetting setting)
        //{
        //    Type = setting.Type;
        //    Value = setting.Value;
        //}

        //public WebsiteSetting(WebsiteSettingCommand command)
        //{
        //    Type = command.Type;
        //    Value = command.Value;
        //}

        public WebsiteSettingTypeEnum Type { get; set; }
        public string Value { get; set; }
    }
}
