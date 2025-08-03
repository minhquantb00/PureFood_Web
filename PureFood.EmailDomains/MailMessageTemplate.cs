using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Table("MailMessageTemplate_tbl")]
    public  class MailMessageTemplate : BaseDomain
    {
        public new string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsActive { get; private set; }
        public Guid? Attachment1FileId { get; private set; }
        public Guid? Attachment2FileId { get; private set; }
        public Guid? Attachment3FileId { get; private set; }
    }
}
