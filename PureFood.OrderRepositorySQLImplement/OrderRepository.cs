using Dapper;
using PureFood.BaseRepositories;
using PureFood.EnumDefine;
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
    public class OrderRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<Order>(dbConnectionFactory),
        IOrderRepository, ISqlDbBaseRepository<Order>
    {
        public async Task<ROrder?> GetById(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<ROrder>("[Orders_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<ROrder[]> GetByIds(string[] ids)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Ids", string.Join(",", ids), System.Data.DbType.String);
                var data = await connection.QueryAsync<ROrder>("[Orders_GetByIds]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<ROrder[]> Gets(OrderGetsQuery query)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.Keyword, System.Data.DbType.String);
                parameters.Add("@PageSize", query.PageSize, System.Data.DbType.Int32);
                parameters.Add("@PageIndex", query.PageIndex, System.Data.DbType.Int32);
                var data = await connection.QueryAsync<ROrder>("[Orders_Gets]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}
