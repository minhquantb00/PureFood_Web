using PureFood.BaseRepositories;
using PureFood.OrderCommands.Queries;
using PureFood.OrderDomains;
using PureFood.OrderReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderRepository
{
    public interface IOrderDetailRepository : ISqlDbBaseRepository<OrderDetail>
    {
        Task<ROrderDetail?> GetById(string id);
        Task<ROrderDetail[]?> GetByIds(string[] ids);
        Task<ROrderDetail[]?> Gets(OrderGetsQuery query);
    }
}
