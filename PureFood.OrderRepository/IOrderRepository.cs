using PureFood.BaseRepositories;
using PureFood.OrderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.OrderRepository
{
    public interface IOrderRepository : ISqlDbBaseRepository<Order>
    {
    }
}
