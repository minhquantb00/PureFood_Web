using Abp.Auditing;
using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Audited]
    [Table("QueuedEmail_tbl")]
    public class QueuedEmail : BaseDomain
    {
        private ICollection<QueuedEmailAttachment> _attachments;
        public new string Id { get; private set; }
        public int Priority { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string ReplyTo { get; private set; }
        public string CC { get; private set; }
        public string Bcc { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public int SentTries { get; private set; }
        public DateTime? SentTime { get; private set; }
        public Guid? EmailAccountId { get; private set; }
        public bool SendManually { get; private set; }
        public bool Opened { get; private set; }
        public DateTime? OpenedTime { get; private set; }
        public virtual EmailAccount EmailAccount { get; private set; }
        public virtual ICollection<QueuedEmailAttachment> Attachments
        {
            get { return _attachments ?? (_attachments = new HashSet<QueuedEmailAttachment>()); }
            protected set { _attachments = value; }
        }
    }
}
