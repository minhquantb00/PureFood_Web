using ProtoBuf;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderCommands.Commands
{
    [ProtoContract]
    public record OrderAddCommand : OrderBaseCommand
    {
        public string Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string PaymentOrderId { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Note { get; set; }
        public string Code { get; set; }
    }
    [ProtoContract]
    public record OrderChangeCommand : OrderBaseCommand
    {
        public string Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string PaymentOrderId { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Note { get; set; }
        public string Code { get; set; }
    }
}
