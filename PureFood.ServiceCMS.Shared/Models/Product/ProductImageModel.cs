using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ServiceCMS.Shared.Models.Product
{
    public class ProductImageModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }
}
