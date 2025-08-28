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
        public Task<ROrder?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ROrder[]> GetByIds(string[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<ROrder[]> Gets(OrderGetsQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<ROrder[]> GetsByStatus(OrderStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<ROrder[]> GetsByStatusAndUserId(OrderStatus status, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ROrder[]> GetsByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
