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
    public class ProductRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<Product>(dbConnectionFactory),
        IProductRepository, ISqlDbBaseRepository<Product>
    {
        public async Task<RProduct?> GetById(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RProduct?>("[Products_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RProduct[]> GetByIds(string[] ids)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), System.Data.DbType.String);
                var data = await connection.QueryAsync<RProduct>("[Products_Gets]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<RProduct[]> Gets(ProductGetsQuery query)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.Keyword, System.Data.DbType.String);
                parameters.Add("@PageIndex", query.PageIndex, System.Data.DbType.Int32);
                parameters.Add("@PageSize", query.PageSize, System.Data.DbType.Int32);
                
                var data = await connection.QueryAsync<RProduct>("[Products_Gets]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}
