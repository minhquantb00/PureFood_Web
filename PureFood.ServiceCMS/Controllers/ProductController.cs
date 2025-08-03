using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureFood.AccountManager.Shared;
using PureFood.BaseApplication.Services;
using PureFood.BaseReadModels;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Queries;
using PureFood.ProductManager.Shared;
using PureFood.ServiceCMS.Mappings;
using PureFood.ServiceCMS.Shared.Requests.Product;

namespace PureFood.ServiceCMS.Controllers
{
    public class ProductController(
        ILogger<ProductController> logger,
        ContextService contextService,
        ICacheService cacheService,
        IProductService productService
    )
        : CmsBaseController(logger, contextService)
    {
        [HttpPost]
        public async Task<BaseResponse<object>> Add([FromBody] ProductAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var result = await productService.Add(new ProductAddCommand
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    AvatarImage = request.AvatarImage,
                    Discount = request.Discount,
                    LoginUid = userInfo.LoginUid,
                    ProductTypeId = request.ProductTypeId,
                    NumberOfSales = request.NumberOfSales,
                    PriceDiscount = request.PriceDiscount,
                    ProcessUid = userInfo.Id,
                    Quantity = request.Quantity,
                    ShortDescription = request.ShortDescription,
                    Title = request.Title
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
        public async Task<BaseResponse<object>> Change([FromBody] ProductChangeRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var result = await productService.Change(new ProductChangeCommand
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    AvatarImage = request.AvatarImage,
                    Discount = request.Discount,
                    LoginUid = userInfo.LoginUid,
                    ProductTypeId = request.ProductTypeId,
                    NumberOfSales = request.NumberOfSales,
                    PriceDiscount = request.PriceDiscount,
                    ProcessUid = userInfo.Id,
                    Quantity = request.Quantity,
                    ShortDescription = request.ShortDescription,
                    Title = request.Title
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
        public async Task<BaseResponse<object>> Gets([FromBody] ProductGetsRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if(request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }

                var userInfo = contextService.UserInfoRequired();

                var result = await productService.Gets(new ProductGetsQuery
                {
                    Keyword = request.Keyword,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });

                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }

                var model = result.Data?.Select(x => ProductMapping.ToModel(x)).ToArray();

                response.Data = model ?? Array.Empty<object>();

                response.SetSuccess();
            });
        }
        [HttpPost]
        public async Task<BaseResponse<object>> Get([FromBody] ProductGetRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var result = await productService.Get(new ProductGetQuery
                {
                    Id = request.Id,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    LogError(result.Message);
                    response.SetFail(result.Message);
                    return;
                }
                response.Data = ProductMapping.ToModel(result.Data!);
                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Delete([FromBody] ProductDeleteRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                if (request == null)
                {
                    LogError("Request is null");
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = contextService.UserInfoRequired();
                var result = await productService.Delete(new ProductDeleteCommand
                {
                    Id = request.Id,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
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
    }
}
