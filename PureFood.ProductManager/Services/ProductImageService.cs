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
    public class ProductImageService(
        ILogger<ProductImageService> logger,
        ContextService contextService,
        IProductImageRepository productImageRepository,
        IProductRepository productRepository,
        IESRepository eSRepository
        ) : BaseService(logger, contextService), IProductImageService
    {
        public async Task<BaseCommandResponse> Add(ProductImageAddCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                var product = await productRepository.GetById(command.ProductId);
                if (product == null)
                {
                    LogError($"Product with Id {command.ProductId} not found");
                    response.SetFail($"Product with Id {command.ProductId} not found");
                    return;
                }
                command.Id = "PI" + new Random().Next(1000000, 9999999).ToString();
                var productImage = new ProductImage(command);
                await productImageRepository.Add(productImage);
                EventAdd(productImage.ToAddEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(ProductImageChangeCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                var productImage = await productImageRepository.GetById(command.Id);
                if (productImage == null)
                {
                    LogError($"ProductImage with Id {command.Id} not found");
                    response.SetFail($"ProductImage with Id {command.Id} not found");
                    return;
                }
                var productImageDomain = new ProductImage(productImage);
                productImageDomain.Change(command);
                await productImageRepository.Change(productImageDomain);
                EventAdd(productImageDomain.ToChangeEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Delete(ProductImageDeleteCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseCommandResponse<RProductImage>> Get(ProductImageGetQuery query)
        {
            return await ProcessCommand<RProductImage>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }
                if (string.IsNullOrEmpty(query.Id))
                {
                    LogError("Query Id is null or empty");
                    response.SetFail("Query Id is null or empty");
                    return;
                }
                var productImage = await productImageRepository.GetById(query.Id);
                if (productImage == null)
                {
                    LogError($"ProductImage with Id {query.Id} not found");
                    response.SetFail($"ProductImage with Id {query.Id} not found");
                    return;
                }

                response.Data = productImage;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProductImage[]>> Gets(ProductImageGetsQuery query)
        {
            return await ProcessCommand<RProductImage[]>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }
                var productImages = await productImageRepository.Gets(query);
                response.Data = productImages;
                response.SetSuccess();
            });
        }
    }
}
