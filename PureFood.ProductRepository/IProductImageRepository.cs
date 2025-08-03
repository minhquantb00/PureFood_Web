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
    public interface IProductImageRepository : ISqlDbBaseRepository<ProductImage>
    {
        Task<RProductImage?> GetById(string id);
        Task<RProductImage[]> GetByIds(string[] ids);
        Task<RProductImage[]> Gets(ProductImageGetsQuery query);
    }
}
