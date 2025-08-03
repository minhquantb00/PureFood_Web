using PureFood.AccountDomains;
using PureFood.AccountReadModels;
using PureFood.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountRepository
{
    public interface IForgotPasswordRepository : ISqlDbBaseRepository<ForgotPassword>
    {
        Task<RForgotPassword?> GetByCode(string code);
    }
}
