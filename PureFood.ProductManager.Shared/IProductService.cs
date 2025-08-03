using PureFood.BaseCommands;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Queries;
using PureFood.ProductReadModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductManager.Shared
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        Task<BaseCommandResponse> Add(ProductAddCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Change(ProductChangeCommand command);
        [OperationContract]
        Task<BaseCommandResponse<RProduct[]>> Gets(ProductGetsQuery query);
        [OperationContract]
        Task<BaseCommandResponse<RProduct>> Get(ProductGetQuery query);
        [OperationContract]
        Task<BaseCommandResponse> Delete(ProductDeleteCommand command);
    }
}
