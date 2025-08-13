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
    public class CartItemRepository(IDbConnectionFactory dbConnectionFactory)
    : SqlDbBaseRepository<CartItem>(dbConnectionFactory),
        ICartItemRepository, ISqlDbBaseRepository<CartItem>
    {
        public async Task<RCartItem[]> GetByCartId(string cartId)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CartId", cartId, System.Data.DbType.String);
                var data = await connection.QueryAsync<RCartItem>("[CartItems_GetByCartId]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data.ToArray();
            });
        }

        public async Task<RCartItem?> GetById(string id)
        {
            return await DbConnectionFactory.WithConnection(async connection =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.String);
                var data = await connection.QueryFirstOrDefaultAsync<RCartItem>("[CartItems_GetById]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            });
        }
    }
}
