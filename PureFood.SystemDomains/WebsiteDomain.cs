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
    [Table("WebsiteDomain_tbl")]
    public class WebsiteDomain : BaseDomain
    {
        public new string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public StatusEnum Status { get; set; }
        public string CompanyId { get; set; }
        public string WebsiteId { get; set; }
        public string Domain { get; set; }
        public HttpProtocolEnum Protocol { get; set; }
        public string? CertificateFileName1 { get; set; }
        public byte[]? CertificateFileData1 { get; set; }
        public string? CertificateFileName2 { get; set; }
        public byte[]? CertificateFileData2 { get; set; }
        public WebsiteDomainTypeEnum Type { get; set; }
        public string FullDomain => $"{Protocol}://{Domain}";
    }
}
