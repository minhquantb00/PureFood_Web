using MimeKit;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailCommands.Commands
{
    [ProtoContract]
public record SendMessageCommand
{
    [ProtoMember(1)] public List<string> To { get; set; }
    [ProtoMember(2)] public string Subject { get; set; }
    [ProtoMember(3)] public string Content { get; set; }

    public SendMessageCommand(IEnumerable<string> to, string subject, string content)
    {
            To = to.ToList();
        Subject = subject;
        Content = content;
    }

    // Parameterless constructor (required for protobuf-net)
    public SendMessageCommand() { }
}

}
