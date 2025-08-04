using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CustomerApi.Shared.Requests.Product
{
    public record ProductReviewAddRequest
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

    public record ProductReviewChangeRequest
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

    public record ProductReviewGetRequest
    {
        public string Id { get; set; }
    }

    public record ProductReviewGetsRequest
    {
        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
