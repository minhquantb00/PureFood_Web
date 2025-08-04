using PureFood.BaseApplication.Services;
using PureFood.BaseCommands;
using PureFood.ESRepositories;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Queries;
using PureFood.ProductDomains;
using PureFood.ProductManager.Shared;
using PureFood.ProductReadModels;
using PureFood.ProductRepository;

namespace PureFood.ProductManager.Services
{
    public class ProductReviewService(
        ILogger<ProductReviewService> logger,
        ContextService contextService,
        IESRepository eSRepository,
        IProductReviewRepository productReviewRepository
        ) : BaseService(logger, contextService), IProductReviewService
    {
        public async Task<BaseCommandResponse> Add(ProductReviewAddCommand command)
        {
            return await ProcessCommand(async response =>
            {
                if(command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                command.Id = "PR" + new Random().Next(1000000, 9999999).ToString();
                var productReview = new ProductReview(command);
                await productReviewRepository.Add(productReview);
                EventAdd(productReview.ToAddEvent());

                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(ProductReviewChangeCommand command)
        {
            return await ProcessCommand(async response =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }

                var data = await productReviewRepository.Get(command.Id);
                if (data == null)
                {
                    LogError("Data is null");
                    response.SetFail("Data is null");
                    return;
                }

                var productReview = new ProductReview(data);
                productReview.Change(command);

                await productReviewRepository.Change(productReview);

                EventAdd(productReview.ToChangeEvent());

                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProductReview>> Get(ProductReviewGetQuery query)
        {
            return await ProcessCommand<RProductReview>(async response =>
            {
                if(query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }

                var data = await productReviewRepository.Get(query.Id);
                if(data == null)
                {
                    LogError("Data is null");
                    response.SetFail("Data is null");
                    return;
                }

                response.Data = data;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProductReview[]>> Gets(ProductReviewGetsQuery query)
        {
            return await ProcessCommand<RProductReview[]>(async response =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }

                var data = await productReviewRepository.Gets(query);
                if (data == null)
                {
                    LogError("Data is null");
                    response.SetFail("Data is null");
                    return;
                }

                response.Data = data;
                response.SetSuccess();
            });
        }
    }
}
