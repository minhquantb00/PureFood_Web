using PureFood.BaseRepositories;
using PureFood.ProductCommands.Queries;
using PureFood.ProductDomains;
using PureFood.ProductReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductRepository
{
    public interface IProductRepository : ISqlDbBaseRepository<Product>
    {
        Task<RProduct?> GetById(string id);
        Task<RProduct[]> GetByIds(string[] ids);
        Task<RProduct[]> Gets(ProductGetsQuery query);
    }
}
