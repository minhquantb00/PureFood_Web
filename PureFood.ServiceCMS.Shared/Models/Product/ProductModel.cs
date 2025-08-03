using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Models.Product
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string ProductTypeId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string AvatarImage { get; set; }
        public string Title { get; set; }
        public int Discount { get; set; }
        public double PriceDiscount { get; set; }
        public int Quantity { get; set; }
        public int NumberOfSales { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public StatusEnum Status { get; set; }
    }
}
