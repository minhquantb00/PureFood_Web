using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderDomains
{
    [Table("OrderDetail_tbl")]
    public class OrderDetail : BaseDomain
    {
        public new string Id { get; private set; }
        public string OrderId { get; private set; }
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImage { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }
    }
}
