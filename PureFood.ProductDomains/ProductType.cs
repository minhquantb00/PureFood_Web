using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductDomains
{
    [Table("ProductType_tbl")]  
    public class ProductType : BaseDomain
    {
        public new string Id { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
    }
}
