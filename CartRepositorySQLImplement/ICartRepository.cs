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
    public interface ICartRepository : ISqlDbBaseRepository<Cart>
    {
        Task<RCart?> GetById(string id);
        Task<RCart[]?> GetByUserId(string userId);
    }
}
