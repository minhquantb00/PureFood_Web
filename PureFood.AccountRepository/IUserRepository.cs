using PureFood.AccountCommands.Queries;
using PureFood.AccountDomains;
using PureFood.AccountReadModels;
using PureFood.BaseReadModels;
using PureFood.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountRepository
{
    public interface IUserRepository : ISqlDbBaseRepository<User>
    {
        Task<RUser?> Get(string id);
        Task<RUser[]> Gets(string[] ids);
        Task<RUser?> GetByUserNameOrEmailOrPhoneNumber(string keyword);
        Task<RUser?> GetByUserName(string userName);
        Task Add(IDbConnection dbConnection, IDbTransaction dbTransaction, User user);
        Task PasswordChange(User user);
        Task TwoFactorChange(User user);
        Task PhoneNumberChange(User user);
        Task<RUser[]> Search(AccountGetsQuery query, RefSqlPaging paging);
        Task ProfileChangeNameAndGender(User user);
        Task AddOrChange(User[] users);
        Task ForgotPasswordTwoFactorVerifySuccess(User user, ForgotPassword forgotPassword);
        Task AuthenticatorSecretKeyAddOrChange(User user);
    }
}
