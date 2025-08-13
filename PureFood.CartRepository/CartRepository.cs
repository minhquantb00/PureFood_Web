using CartRepositorySQLImplement;
using Dapper;
using PureFood.BaseRepositories;
using PureFood.CartDomains;
using PureFood.CartReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartRepository
{
    public class CartRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<Cart>(dbConnectionFactory),
        ICartRepository, ISqlDbBaseRepository<Cart>
    {
        public async Task<RCart?> GetById(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RCart>("[Carts_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task<RCart[]> GetByUserId(string userId)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, System.Data.DbType.String);
                var data = await connection.QueryAsync<RCart>("[Carts_GetByUserId]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }
    }
}
