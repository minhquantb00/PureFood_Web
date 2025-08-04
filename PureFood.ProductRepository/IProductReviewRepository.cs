using PureFood.BaseCommands;
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
    public interface IProductReviewRepository : ISqlDbBaseRepository<ProductReview>
    {
        Task<RProductReview?> Get(string id);
        Task<RProductReview[]?> GetByIds(string[]? ids);
        Task<RProductReview[]?> Gets(ProductReviewGetsQuery query);
    }
}
