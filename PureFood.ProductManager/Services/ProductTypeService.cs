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
    public class ProductTypeService(
        ILogger<ProductTypeService> logger,
        ContextService contextService,
        IProductTypeRepository productTypeRepository,
        IESRepository eSRepository
        ) : BaseService(logger, contextService), IProductTypeService
    {
        public async Task<BaseCommandResponse> Add(ProductTypeAddCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                command.Id = "PT" + new Random().Next(1000000, 9999999).ToString();
                var productType = new ProductType(command);
                await productTypeRepository.Add(productType);
                EventAdd(productType.ToAddEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(ProductTypeChangeCommand command)
        {
            return await ProcessCommand(async (response) =>
            {
                if (command == null)
                {
                    LogError("Command is null");
                    response.SetFail("Command is null");
                    return;
                }
                var productType = await productTypeRepository.GetById(command.Id);
                if (productType == null)
                {
                    LogError($"ProductType with Id {command.Id} not found");
                    response.SetFail($"ProductType with Id {command.Id} not found");
                    return;
                }
                var productTypeDomain = new ProductType(productType);
                productTypeDomain.Change(command);
                await productTypeRepository.Change(productTypeDomain);
                EventAdd(productTypeDomain.ToChangeEvent());
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProductType>> Get(ProductTypeGetQuery query)
        {
            return await ProcessCommand<RProductType>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }
                var productType = await productTypeRepository.GetById(query.Id);
                if (productType == null)
                {
                    LogError($"ProductType with Id {query.Id} not found");
                    response.SetFail($"ProductType with Id {query.Id} not found");
                    return;
                }
                response.Data = productType;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProductType[]>> GetByIds(ProductTypeGetByIdsQuery query)
        {
            return await ProcessCommand<RProductType[]>(async (response) =>
            {
                if (query == null || query.Ids == null || query.Ids.Length == 0)
                {
                    LogError("Query or Ids are null or empty");
                    response.SetFail("Query or Ids are null or empty");
                    return;
                }
                var productTypes = await productTypeRepository.GetByIds(query.Ids);
                if (productTypes == null || productTypes.Length == 0)
                {
                    LogError($"No ProductTypes found for Ids: {string.Join(", ", query.Ids)}");
                    response.SetFail($"No ProductTypes found for Ids: {string.Join(", ", query.Ids)}");
                    return;
                }
                response.Data = productTypes;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RProductType[]>> Gets(ProductTypeGetsQuery query)
        {
            return await ProcessCommand<RProductType[]>(async (response) =>
            {
                if (query == null)
                {
                    LogError("Query is null");
                    response.SetFail("Query is null");
                    return;
                }
                var productTypes = await productTypeRepository.Gets(query);
                if (productTypes == null || productTypes.Length == 0)
                {
                    LogError("No ProductTypes found");
                    response.SetFail("No ProductTypes found");
                    return;
                }
                response.Data = productTypes;
                response.SetSuccess();
            });
        }
    }
}
