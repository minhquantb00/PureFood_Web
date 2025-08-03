using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Table("QueuedEmailAttachment_tbl")]
    public class QueuedEmailAttachment : BaseDomain
    {
        public new string Id { get; private set; }
        public string QueuedEmailId { get; private set; }
        public EmailAttachmentStorageLocation StorageLocation { get; private set; }
        public string Path { get; private set; }
        public int? FileId { get; private set; }
        [Obsolete("Use property MediaStorage instead")]
        public byte[] Data { get; private set; }
        public string Name { get; private set; }
        public string MimeType { get; private set; }
        public Guid? MediaStorageId { get; private set; }
    }
}
