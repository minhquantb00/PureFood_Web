using Microsoft.AspNetCore.Mvc;
using PureFood.BaseApplication.Services;
using PureFood.BaseReadModels;
using PureFood.CartManager.Shared;
using PureFood.CustomerApi.Mappings;
using PureFood.CustomerApi.Shared.Requests.Cart;
using PureFood.EnumDefine;
using System.Reflection.Metadata.Ecma335;

namespace PureFood.CustomerApi.Controllers
{
    public class CartItemController(
        ILogger<CartItemController> logger,
        ContextService contextService,
        ICartItemService cartItemService
        ) : BaseApiController(logger, contextService)
    {
        [HttpPost]
        public async Task<BaseResponse<object>> Add([FromBody] CartItemAddRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }

                var userInfo = ContextService.UserInfoRequired();

                var result = await cartItemService.Add(new CartCommands.Commands.CartItemAddCommand
                {
                    CartId = request.CartId,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                });

                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Change([FromBody] CartItemChangeRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if(request == null)
                {
                    LogError(ErrorCodeEnum.NullRequestExceptions.ToString());
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }

                var userInfo = ContextService.UserInfoRequired();
                var result = await cartItemService.Change(new CartCommands.Commands.CartItemChangeCommand
                {
                    Id = request.CartId,
                    CartId = request.CartId,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                });

                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Delete([FromBody] CartItemDeleteRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if (request == null)
                {
                    LogError(ErrorCodeEnum.NullRequestExceptions.ToString());
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }

                var userInfo = ContextService.UserInfoRequired();
                var result = await cartItemService.Delete(new CartCommands.Commands.CartItemRemoveCommand
                {
                    Id = request.Id
                });

                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> GetByCartId([FromBody] CartItemGetByCartIdRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if(request == null)
                {
                    LogError(ErrorCodeEnum.NullRequestExceptions.ToString());
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }

                var userInfo = ContextService.UserInfoRequired();
                var result = await cartItemService.GetByCartId(new CartCommands.Queries.CartItemGetByCartIdQuery
                {
                    CartId = request.CartId,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });

                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }

                response.Data = result.Data?.Select(x => CartItemMapping.ToModel(x)).ToArray();

                response.SetSuccess();
            });
        }
    }
}
