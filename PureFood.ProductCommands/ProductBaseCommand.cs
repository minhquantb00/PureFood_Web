using ProtoBuf;
using PureFood.BaseCommands;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductCommands
{
    [ProtoContract]
    [ProtoInclude(200, typeof(ProductAddCommand))]
    [ProtoInclude(300, typeof(ProductChangeCommand))]
    [ProtoInclude(400, typeof(ProductGetQuery))]
    [ProtoInclude(500, typeof(ProductGetsQuery))]
    [ProtoInclude(600, typeof(ProductDeleteCommand))]
    [ProtoInclude(700, typeof(ProductImageGetQuery))]
    [ProtoInclude(800, typeof(ProductImageGetsQuery))]
    [ProtoInclude(900, typeof(ProductImageAddCommand))]
    [ProtoInclude(1000, typeof(ProductImageChangeCommand))]
    [ProtoInclude(1100, typeof(ProductImageDeleteCommand))]
    public record ProductBaseCommand : BaseCommand
    {
        [ProtoMember(101)] public override string? ObjectId { get; set; }
        [ProtoMember(102)] public override string? ProcessUid { get; set; }
        [ProtoMember(103)] public override DateTime ProcessDate { get; set; }
        [ProtoMember(104)] public override string? LoginUid { get; set; }
    }
}
