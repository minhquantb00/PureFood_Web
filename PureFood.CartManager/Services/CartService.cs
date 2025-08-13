using CartRepositorySQLImplement;
using PureFood.BaseApplication.Services;
using PureFood.BaseCommands;
using PureFood.CartCommands.Commands;
using PureFood.CartCommands.Events;
using PureFood.CartCommands.Queries;
using PureFood.CartDomains;
using PureFood.CartManager.Shared;
using PureFood.CartReadModels;
using PureFood.EnumDefine;

namespace PureFood.CartManager.Services
{
    public class CartService(
        ILogger<CartService> logger,
        ContextService contextService,
        ICartRepository cartRepository
    ) : BaseService(logger, contextService), ICartService
    {
        public async Task<BaseCommandResponse> Add(CartAddCommand command)
        {
            return await ProcessCommand(async response =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    return;
                }

                command.Id = "C" + new Random().Next(100000, 999999).ToString();
                var cart = new Cart(command);

                await cartRepository.Add(cart);

                EventAdd(new CartAddEvent
                {
                    ObjectId = command.Id,
                });

                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RCart>> GetByUserId(CartGetByUserIdQuery query)
        {
            return await ProcessCommand<RCart>(async response =>
            {
                if(query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }

                var userInfo = contextService.UserInfoRequired();

                query.UserId = userInfo.Id;
                var cart = await cartRepository.GetByUserId(query.UserId);
                if(cart == null)
                {
                    LogError("Cart is null for user: " + query.UserId);
                    response.SetFail("Cart is null");
                    return;
                }

                response.Data = cart.FirstOrDefault();
                response.SetSuccess();
            });
        }
    }
}
