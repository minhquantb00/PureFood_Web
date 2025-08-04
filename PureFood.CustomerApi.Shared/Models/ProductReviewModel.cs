using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CustomerApi.Shared.Models
{
    public record ProductReviewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
