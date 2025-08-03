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
    public class ProductTypeController(
        ILogger<ProductTypeController> logger,
        ContextService contextService,
        ICacheService cacheService,
        IProductTypeService productService
    )
        : CmsBaseController(logger, contextService)
    {
        [HttpPost]
        public async Task<BaseResponse<object>> Add([FromBody] ProductTypeAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Add(new ProductCommands.Commands.ProductTypeAddCommand
                {
                    Name = request.Name,
                    ImageUrl = request.ImageUrl,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    LogError($"Add product type failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.SetSuccess();
            });
        }
        [HttpPost]
        public async Task<BaseResponse<object>> Change([FromBody] ProductTypeChangeRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Change(new ProductCommands.Commands.ProductTypeChangeCommand
                {
                    Id = request.Id,
                    Name = request.Name,
                    ImageUrl = request.ImageUrl,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    LogError($"Change product type failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Get([FromBody] ProductTypeGetRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Get(new ProductCommands.Queries.ProductTypeGetQuery
                {
                    Id = request.Id,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    LogError($"Get product type failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = ProductTypeMapping.ToModel(result.Data!);
                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Gets([FromBody] ProductTypeGetsRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Gets(new ProductCommands.Queries.ProductTypeGetsQuery
                {
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    LogError($"Get product types failed: {result.ErrorCode} - {string.Join(", ", result.Messages)}");
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = result.Data?.Select(ProductTypeMapping.ToModel);
                response.SetSuccess();
            });
        }
    }
}
