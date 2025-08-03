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
    public interface IProductTypeRepository : ISqlDbBaseRepository<ProductType>
    {
        Task<RProductType?> GetById(string id);
        Task<RProductType[]> GetByIds(string[] ids);
        Task<RProductType[]> Gets(ProductTypeGetsQuery query);
    }
}
