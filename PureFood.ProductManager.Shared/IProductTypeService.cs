using PureFood.BaseCommands;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Queries;
using PureFood.ProductReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductManager.Shared
{
    [ServiceContract]
    public interface IProductTypeService
    {
        [OperationContract]
        Task<BaseCommandResponse> Add(ProductTypeAddCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Change(ProductTypeChangeCommand command);
        [OperationContract]
        Task<BaseCommandResponse<RProductType[]>> Gets(ProductTypeGetsQuery query);
        [OperationContract]
        Task<BaseCommandResponse<RProductType>> Get(ProductTypeGetQuery query);
        [OperationContract]
        Task<BaseCommandResponse<RProductType[]>> GetByIds(ProductTypeGetByIdsQuery query);
    }
}
