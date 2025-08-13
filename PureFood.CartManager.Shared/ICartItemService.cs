using PureFood.BaseCommands;
using PureFood.CartCommands.Commands;
using PureFood.CartCommands.Queries;
using PureFood.CartReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartManager.Shared
{
    [ServiceContract                                           ]
    public interface ICartItemService
    {
        [OperationContract]
        Task<BaseCommandResponse> Add(CartItemAddCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Change(CartItemChangeCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Delete(CartItemRemoveCommand command);
        [OperationContract]
        Task<BaseCommandResponse<RCartItem[]?>> GetByCartId(CartItemGetByCartIdQuery query);
    }
}
