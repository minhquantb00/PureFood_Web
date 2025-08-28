using PureFood.BaseApplication.Services;
using PureFood.ESRepositories;
using PureFood.OrderManager.Shared;
using PureFood.OrderRepository;

namespace PureFood.OrderManager.Services
{
    public class OrderService(
        ILogger<OrderService> logger,
        ContextService contextService,
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IESRepository esRepository
        ) : BaseService(logger, contextService), IOrderService
    {
    }
}
