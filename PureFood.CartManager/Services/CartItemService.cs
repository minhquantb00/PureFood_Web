using CartRepositorySQLImplement;
using PureFood.BaseApplication.Services;
using PureFood.BaseCommands;
using PureFood.CartCommands.Commands;
using PureFood.CartCommands.Events;
using PureFood.CartCommands.Queries;
using PureFood.CartDomains;
using PureFood.CartManager.Shared;
using PureFood.CartReadModels;

namespace PureFood.CartManager.Services
{
    public class CartItemService(ILogger<CartItemService> logger, ContextService contextService, ICartItemRepository cartItemRepository, ICartRepository cartRepository) : BaseService(logger, contextService), ICartItemService
    {
        public async Task<BaseCommandResponse> Add(CartItemAddCommand command)
        {
            return await ProcessCommand(async response =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                command.Id = "CI" + new Random().Next(100000, 999999).ToString();
                var cartItem = new CartItem(command);
                await cartItemRepository.Add(cartItem);
                EventAdd(new CartItemAddEvent
                {
                    ObjectId = command.Id,
                });
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(CartItemChangeCommand command)
        {
            return await ProcessCommand(async response =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var cartItem = await cartItemRepository.GetById(command.Id);
                if(cartItem == null)
                {
                    response.SetFail("Cart item not found");
                    return;
                }
                var cartDomain = new CartItem(cartItem);
                cartDomain.Change(command);
                await cartItemRepository.Change(cartDomain);
                EventAdd(new CartItemChangeEvent
                {
                    ObjectId = command.Id,
                });
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Delete(CartItemRemoveCommand command)
        {
            return await ProcessCommand(async response =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                var userInfo = contextService.UserInfoRequired();
                var cartItem = await cartItemRepository.GetById(command.Id);
                if(cartItem == null)
                {
                    response.SetFail("Cart item not found");
                    return;
                }
                var cartDomain = new CartItem(cartItem);
                await cartItemRepository.Remove(cartDomain);
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RCartItem[]?>> GetByCartId(CartItemGetByCartIdQuery query)
        {
            return await ProcessCommand<RCartItem[]?>(async response =>
            {
                if(query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var cartItems = await cartItemRepository.GetByCartId(query.CartId);
                if(cartItems == null || !cartItems.Any())
                {
                    response.SetFail("No cart items found for the given CartId");
                    return;
                }

                response.Data = cartItems;
                response.SetSuccess();
            });
        }
    }
}
