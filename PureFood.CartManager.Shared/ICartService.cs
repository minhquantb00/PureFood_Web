using PureFood.BaseCommands;
using PureFood.CartCommands.Commands;
using PureFood.CartCommands.Queries;
using PureFood.CartDomains;
using PureFood.CartReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartManager.Shared
{
    [ServiceContract]
    public interface ICartService
    {
        [OperationContract]
        Task<BaseCommandResponse> Add(CartAddCommand command);
        [OperationContract]
        Task<BaseCommandResponse<RCart>> GetByUserId(CartGetByUserIdQuery query);    }
}
