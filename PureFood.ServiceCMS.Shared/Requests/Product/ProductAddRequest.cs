using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Requests.Product
{
    public record ProductAddRequest
    {
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
    }

    public record ProductChangeRequest
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
    }

    public record ProductGetRequest
    {
        public string Id { get; set; }
    }

    public record ProductGetsRequest
    {
        public string? Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public record ProductDeleteRequest
    {
        public string Id { get; set; }
    }
}
