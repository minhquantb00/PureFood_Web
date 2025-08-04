using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureFood.BaseApplication.Services;
using PureFood.BaseReadModels;
using PureFood.CustomerApi.Mappings;
using PureFood.CustomerApi.Shared.Requests.Product;
using PureFood.ProductManager.Shared;
using PureFood.ServiceCMS.Controllers;

namespace PureFood.CustomerApi.Controllers
{
    public class ProductReviewController(
        ILogger<ProductReviewController> logger,
        ContextService contextService,
        ICacheService cacheService,
        IProductReviewService productService
    )
        : CmsBaseController(logger, contextService)
    {
        [HttpPost]
        public async Task<BaseResponse<object>> Add([FromBody] ProductReviewAddRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if (request == null)
                {
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Add(new ProductCommands.Commands.ProductReviewAddCommand
                {
                    Comment = request.Comment,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    ProductId = request.ProductId,
                    UserId = request.UserId,
                    Rating = request.Rating
                });

                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Change([FromBody] ProductReviewChangeRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if (request == null)
                {
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Change(new ProductCommands.Commands.ProductReviewChangeCommand
                {
                    Comment = request.Comment,
                    Id = request.Id,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id,
                    ProductId = request.ProductId,
                    UserId = request.UserId,
                    Rating = request.Rating
                });

                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> GetById([FromBody] ProductReviewGetRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if (request == null)
                {
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Get(new ProductCommands.Queries.ProductReviewGetQuery
                {
                    Id = request.Id,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }

                response.Data = ProductReviewMapping.ToModel(result.Data!);

                response.SetSuccess();
            });
        }

        [HttpPost]
        public async Task<BaseResponse<object>> Gets([FromBody] ProductReviewGetsRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                if (request == null)
                {
                    response.SetFail(EnumDefine.ErrorCodeEnum.NullRequestExceptions);
                    return;
                }
                var userInfo = ContextService.UserInfoRequired();
                var result = await productService.Gets(new ProductCommands.Queries.ProductReviewGetsQuery
                {
                    Keyword = request.Keyword,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    LoginUid = userInfo.LoginUid,
                    ProcessUid = userInfo.Id
                });
                if (!result.Status)
                {
                    response.SetFail(result.Message);
                    return;
                }

                response.Data = result.Data?.Select(x => ProductReviewMapping.ToModel(x)).ToArray();

                response.SetSuccess();
            });
        }
    }
}
