using Dapper;
using PureFood.BaseRepositories;
using PureFood.ProductCommands.Queries;
using PureFood.ProductDomains;
using PureFood.ProductReadModels;
using PureFood.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductRepositorySQLImplement
{
    public class ProductImageRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<ProductImage>(dbConnectionFactory),
        IProductImageRepository, ISqlDbBaseRepository<ProductImage>
    {
        public async Task<RProductImage?> GetById(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RProductImage>("[ProductImages_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RProductImage[]> GetByIds(string[] ids)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), System.Data.DbType.String);
                var data = await connection.QueryAsync<RProductImage>("[ProductImages_GetByIds]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<RProductImage[]> Gets(ProductImageGetsQuery query)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.Keyword, System.Data.DbType.String);
                parameters.Add("@PageSize", query.PageSize, System.Data.DbType.Int32);
                parameters.Add("@PageIndex", query.PageIndex, System.Data.DbType.Int32);

                var data = await connection.QueryAsync<RProductImage>("[ProductImages_Gets]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}
