using PureFood.BaseRepositories;
using PureFood.CartDomains;
using PureFood.CartReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartRepositorySQLImplement
{
    public interface ICartItemRepository : ISqlDbBaseRepository<CartItem>
    {
        Task<RCartItem?> GetById(string id);
        Task<RCartItem[]> GetByCartId(string cartId);
    }
}
