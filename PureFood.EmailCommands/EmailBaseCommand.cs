using ProtoBuf;
using PureFood.BaseCommands;
using PureFood.EmailCommands.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(EmailCreateQueueCommand))]
    [ProtoInclude(300, typeof(EmailCreateQueuedFromTemplateCommand))]
    public record EmailBaseCommand : BaseCommand
    {
        [ProtoMember(101)] public override string? ObjectId { get; set; }
        [ProtoMember(102)] public override string? ProcessUid { get; set; }
        [ProtoMember(103)] public override DateTime ProcessDate { get; set; }
        [ProtoMember(104)] public override string? LoginUid { get; set; }
    }
}
