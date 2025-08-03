using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailCommands.Commands
{
    [ProtoContract]
    public record EmailCreateQueueCommand : EmailBaseCommand
    {
        [ProtoMember(1)] public string UserId { get; set; }
        [ProtoMember(2)] public int Priority { get; set; }
        [ProtoMember(3)] public string From { get; set; }
        [ProtoMember(4)] public string To { get; set; }
        [ProtoMember(5)] public string ReplyTo { get; set; }
        [ProtoMember(6)] public string CC { get; set; }
        [ProtoMember(7)] public string Bcc { get; set; }
        [ProtoMember(8)] public string Subject { get; set; }
        [ProtoMember(9)] public string Body { get; set; }
        [ProtoMember(10)] public int SentTries { get; set; }
        [ProtoMember(11)] public DateTime? SentTime { get; set; }
        [ProtoMember(12)] public string EmailAccountId { get; set; }
        [ProtoMember(13)] public bool SendManually { get; set; }
        [ProtoMember(14)] public bool Opened { get; set; }
        [ProtoMember(15)] public DateTime? OpenedTime { get; set; }
        [ProtoMember(16)] public DateTime CreatedTime { get; set; }
    }

    [ProtoContract]
    public record EmailCreateQueuedFromTemplateCommand : EmailBaseCommand
    {
        [ProtoMember(1)] public string UserId { get; set; }
        [ProtoMember(2)] public string MailMessageTemplateId { get; set; }
        [ProtoMember(3)] public string MailMessageTemplateName { get; set; }
        [ProtoMember(4)] public object Model { get; set; }
        [ProtoMember(5)] public int Priority { get; set; }
        [ProtoMember(6)] public string From { get; set; }
        [ProtoMember(7)] public string To { get; set; }
        [ProtoMember(8)] public string ReplyTo { get; set; }
        [ProtoMember(9)] public string CC { get; set; }
        [ProtoMember(10)] public string Bcc { get; set; }
        [ProtoMember(11)] public string Subject { get; set; }
        [ProtoMember(12)] public string Body { get; set; }
        [ProtoMember(13)] public DateTime CreatedTime { get; set; }
        [ProtoMember(14)] public int SentTries { get; set; }
        [ProtoMember(15)] public DateTime? SentTime { get; set; }
        [ProtoMember(16)] public Guid? EmailAccountId { get; set; }
        [ProtoMember(17)] public bool SendManually { get; set; }
    }
}
