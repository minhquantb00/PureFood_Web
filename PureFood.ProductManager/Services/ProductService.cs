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
    public class ProductService(
        ILogger<ProductService> logger,
        ContextService contextService,
        IESRepository eSRepository,
        IProductRepository productRepository
        ) : BaseService(logger, contextService), IProductService
    {
        public async Task<BaseCommandResponse> Add(ProductAddCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                command.Id = "P" + new Random().Next(1000000, 9999999).ToString();
                var product = new Product(command);
                await productRepository.Add(product);
                EventAdd(product.ToAddEvent()); 
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(ProductChangeCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                var product = await productRepository.GetById(command.Id);
                if (product == null)
                {
                    LogError($"Product with Id {command.Id} not found");
                    response.SetFail($"Product with Id {command.Id} not found");
                    return;
                }
                var productDomain = new Product(product);
                productDomain.Change(command);
                await productRepository.Change(productDomain);
                EventAdd(productDomain.ToChangeEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Delete(ProductDeleteCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                if (string.IsNullOrEmpty(command.Id))
                {
                    LogError("Command Id is null or empty");
                    response.SetFail("Command Id is null or empty");
                    return;
                }
                var product = await productRepository.GetById(command.Id);
                if (product == null)
                {
                    LogError($"Product with Id {command.Id} not found");
                    response.SetFail($"Product with Id {command.Id} not found");
                    return;
                }
                var productDomain = new Product(product);
                productDomain.Delete(command);
                await productRepository.Change(productDomain);
                EventAdd(productDomain.ToChangeEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProduct>> Get(ProductGetQuery query)
        {
            return await ProcessCommand<RProduct>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }

                if(string.IsNullOrEmpty(query.Id))
                {
                    LogError("Query Id is null or empty");
                    response.SetFail("Query Id is null or empty");
                    return;
                }

                var result = await productRepository.GetById(query.Id);
                if(result == null)
                {
                    LogError($"Not found");
                    response.SetFail($"Not found");
                    return;
                }

                response.Data = result;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProduct[]>> Gets(ProductGetsQuery query)
        {
            return await ProcessCommand<RProduct[]>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }
                var result = await productRepository.Gets(query);
                if (result == null || !result.Any())
                {
                    LogError($"Not found");
                    response.SetFail($"Not found");
                    return;
                }
                response.Data = result.ToArray();
                response.SetSuccess();
            });
        }
    }
}
