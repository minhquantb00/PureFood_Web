using Microsoft.AspNetCore.Mvc;
using PureFood.BaseApplication.Services;
using PureFood.BaseReadModels;
using PureFood.CartCommands.Commands;
using PureFood.CartCommands.Queries;
using PureFood.CartManager.Shared;
using PureFood.CustomerApi.Mappings;
using PureFood.CustomerApi.Shared.Requests.Cart;

namespace PureFood.CustomerApi.Controllers
{
    public class CartController(
        ILogger<CartController> logger,
        ContextService contextService,
        ICacheService cacheService, 
        ICartService cartService
        ) : BaseApiController(logger, contextService)
    {

        [HttpPost]
        public async Task<BaseResponse<object>> Add([FromBody] CartAddRequest request)
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
                var result = await cartService.Add(new CartAddCommand
                {
                    UserId = request.UserId,
                });
                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message, result.ErrorCode);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> GetByUserId([FromBody] CartGetByUserIdRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var result = await cartService.GetByUserId(new CartGetByUserIdQuery
                {
                    UserId = userInfo.Id,
                });
                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message, result.ErrorCode);
                    return;
                }

                response.Data = CartMapping.ToModel(result.Data!);
                response.SetSuccess();
            });
        }
    }
}
