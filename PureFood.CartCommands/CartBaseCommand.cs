using ProtoBuf;
using PureFood.BaseCommands;
using PureFood.CartCommands.Commands;
using PureFood.CartCommands.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(CartAddCommand))]
    [ProtoInclude(300, typeof(CartItemChangeCommand))]
    [ProtoInclude(400, typeof(CartItemRemoveCommand))]
    [ProtoInclude(500, typeof(CartItemAddCommand))]
    [ProtoInclude(600, typeof(CartGetByUserIdQuery))]
    [ProtoInclude(700, typeof(CartItemGetByCartIdQuery))]
    public record CartBaseCommand : BaseCommand
    {
        [ProtoMember(101)] public override string? ObjectId { get; set; }
        [ProtoMember(102)] public override string? ProcessUid { get; set; }
        [ProtoMember(103)] public override DateTime ProcessDate { get; set; }
        [ProtoMember(104)] public override string? LoginUid { get; set; }
    }
}
