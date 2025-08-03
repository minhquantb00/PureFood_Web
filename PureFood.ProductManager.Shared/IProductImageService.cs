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
    public interface IProductImageService
    {
        [OperationContract]
        Task<BaseCommandResponse> Add(ProductImageAddCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Change(ProductImageChangeCommand command);
        [OperationContract]
        Task<BaseCommandResponse<RProductImage[]>> Gets(ProductImageGetsQuery query);
        [OperationContract]
        Task<BaseCommandResponse<RProductImage>> Get(ProductImageGetQuery query);
        [OperationContract]
        Task<BaseCommandResponse> Delete(ProductImageDeleteCommand command);
    }
}
