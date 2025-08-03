using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Requests.Product
{
    public record ProductTypeAddRequest
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }

    public record ProductTypeChangeRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }

    public record ProductTypeGetRequest
    {
        public string Id { get; set; }
    }

    public record ProductTypeGetsRequest
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
