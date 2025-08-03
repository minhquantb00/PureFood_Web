using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureFood.BaseApplication.Services;
using PureFood.BaseReadModels;
using PureFood.EnumDefine;
using PureFood.ProductManager.Shared;
using PureFood.ServiceCMS.Mappings;
using PureFood.ServiceCMS.Shared.Requests.Product;

namespace PureFood.ServiceCMS.Controllers
{
    public class ProductImageController(
        ILogger<ProductImageController> logger,
        ContextService contextService,
        ICacheService cacheService,
        IProductImageService productService
    )
        : CmsBaseController(logger, contextService)
    {
        [HttpPost]
        public async Task<BaseResponse<object>> Add([FromBody] ProductImageAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Add(new ProductCommands.Commands.ProductImageAddCommand
                {
                    ImageUrl = request.ImageUrl,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    ProductId = request.ProductId,
                    Title = request.Title,
                });

                if(!result.Status)
                {
                    LogError($"Add product image failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Change([FromBody] ProductImageChangeRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Change(new ProductCommands.Commands.ProductImageChangeCommand
                {
                    ImageUrl = request.ImageUrl,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    ProductId = request.ProductId,
                    Title = request.Title,
                    Id = request.Id
                });
                if(!result.Status)
                {
                    LogError($"Change product image failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Gets([FromBody] ProductImageGetsRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Gets(new ProductCommands.Queries.ProductImageGetsQuery
                {
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    Keyword = request.Keyword,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                });
                if(!result.Status)
                {
                    LogError($"Get product images failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = result.Data?.Select(x => ProductImageMapping.ToModel(x)).ToArray();
                response.SetSuccess();
                
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Get([FromBody] ProductImageGetRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Get(new ProductCommands.Queries.ProductImageGetQuery
                {
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    Id = request.Id
                });
                if(!result.Status)
                {
                    LogError($"Get product image failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = ProductImageMapping.ToModel(result.Data!);
                response.SetSuccess();
            });
        }
    }
}
