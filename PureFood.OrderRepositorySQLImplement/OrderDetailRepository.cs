using Dapper;
using PureFood.BaseRepositories;
using PureFood.OrderCommands.Queries;
using PureFood.OrderDomains;
using PureFood.OrderReadModels;
using PureFood.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderRepositorySQLImplement
{
    public class OrderDetailRepository(IDbConnectionFactory dbConnectionFactory)
        : SqlDbBaseRepository<OrderDetail>(dbConnectionFactory),
          IOrderDetailRepository, ISqlDbBaseRepository<OrderDetail>
    {
        public async Task<ROrderDetail?> GetById(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<ROrderDetail>("[OrderDetails_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<ROrderDetail[]?> GetByIds(string[] ids)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), System.Data.DbType.String);
                var data = await connection.QueryAsync<ROrderDetail>("[OrderDetails_GetByIds]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<ROrderDetail[]?> Gets(OrderGetsQuery query)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.Keyword, System.Data.DbType.String);
                parameters.Add("@PageIndex", query.PageIndex, System.Data.DbType.Int32);
                parameters.Add("@PageSize", query.PageSize, System.Data.DbType.Int32);
                var data = await connection.QueryAsync<ROrderDetail>("[OrderDetails_Gets]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}
