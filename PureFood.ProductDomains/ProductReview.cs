using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductDomains
{
    [Table("ProductReview_tbl")]
    public class ProductReview : BaseDomain
    {
        public new string Id { get; private set; }
        public string ProductId { get; private set; }
        public string UserId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
    }
}
