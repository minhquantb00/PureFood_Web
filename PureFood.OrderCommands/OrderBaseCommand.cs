using ProtoBuf;
using PureFood.BaseCommands;
using PureFood.OrderCommands.Commands;
using PureFood.OrderCommands.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(OrderGetsQuery))]
    [ProtoInclude(300, typeof(OrderGetByIdQuery))]
    [ProtoInclude(400, typeof(OrderGetByIdsQuery))]
    [ProtoInclude(500, typeof(OrderAddCommand))]
    [ProtoInclude(600, typeof(OrderChangeCommand))]
    public record OrderBaseCommand : BaseCommand
    {
        [ProtoMember(101)] public override string? ObjectId { get; set; }
        [ProtoMember(102)] public override string? ProcessUid { get; set; }
        [ProtoMember(103)] public override DateTime ProcessDate { get; set; }
        [ProtoMember(104)] public override string? LoginUid { get; set; }
    }
}
