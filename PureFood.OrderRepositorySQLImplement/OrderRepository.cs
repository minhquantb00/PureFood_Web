using PureFood.BaseRepositories;
using PureFood.OrderDomains;
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
    }
}
