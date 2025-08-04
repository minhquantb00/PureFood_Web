using Dapper;
using PureFood.BaseRepositories;
using PureFood.ProductCommands.Queries;
using PureFood.ProductDomains;
using PureFood.ProductReadModels;
using PureFood.ProductRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductRepositorySQLImplement
{
    public class ProductReviewRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<ProductReview>(dbConnectionFactory),
        IProductReviewRepository, ISqlDbBaseRepository<ProductReview>
    {
        public async Task<RProductReview?> Get(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RProductReview?>("[ProductReview_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RProductReview[]?> GetByIds(string[]? ids)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), DbType.String);
                var data = await connection.QueryAsync<RProductReview>("[ProductReview_GetByIds]", parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<RProductReview[]?> Gets(ProductReviewGetsQuery query)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.Keyword, DbType.String);
                parameters.Add("@PageIndex", query.PageIndex, DbType.Int32);
                parameters.Add("@PageSize", query.PageSize, DbType.Int32);
                var data = await connection.QueryAsync<RProductReview>("[ProductReview_Gets]", parameters, commandType: CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}
