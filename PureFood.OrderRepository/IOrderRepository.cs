using PureFood.BaseRepositories;
using PureFood.EnumDefine;
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
    public interface IOrderRepository : ISqlDbBaseRepository<Order>
    {
        Task<ROrder?> GetById(string id);
        Task<ROrder[]> GetByIds(string[] ids);
        Task<ROrder[]> Gets(OrderGetsQuery query);
    }
}
