using PureFood.AccountCommands.Commands;
using PureFood.AccountCommands.Queries;
using PureFood.AccountDomains;
using PureFood.AccountReadModels;
using PureFood.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountManager.Shared
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        Task<BaseCommandResponse<RUser[]?>> Gets(AccountGetsQuery query);

        [OperationContract]
        Task<BaseCommandResponse<RUser>> GetById(AccountGetByIdQuery query);

        [OperationContract]
        Task<BaseCommandResponse<RUser[]>> GetByIds(AccountGetByIdsQuery query);

        [OperationContract]
        Task<BaseCommandResponse<RUser>> GetByPhoneNumber(AccountGetByPhoneNumberQuery query);

        [OperationContract]
        Task<BaseCommandResponse> RegisterUser(AccountRegisterUserCommand command);

        [OperationContract]
        Task<BaseCommandResponse<RLoginModel>> Login(AccountLoginCommand command);

        [OperationContract]
        Task<BaseCommandResponse> ChangePassword(AccountChangePasswordCommand command);
        [OperationContract]
        Task<BaseCommandResponse> ForgotPassword(AccountForgotPasswordCommand command);
        [OperationContract]
        Task<BaseCommandResponse> ResetPassword(AccountResetPasswordCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Change(AccountChangeCommand command);
    }
}
