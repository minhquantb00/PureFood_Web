using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Requests.Product
{
    public record ProductImageAddRequest
    {
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
    }

    public record ProductImageChangeRequest
    {
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
    }
    public record ProductImageGetRequest
    {
        public string Id { get; set; }  
    }

    public record ProductImageGetsRequest
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
