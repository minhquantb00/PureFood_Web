using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderDomains
{
    [Table("Order_tbl")]
    public class Order : BaseDomain
    {
        public new string Id { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public string PaymentOrderId { get; private set; }
        public string UserId { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public decimal ActualPrice { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public string Note { get; private set; }
        public string Code { get; private set; }
    }
}
