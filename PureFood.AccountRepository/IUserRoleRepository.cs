using PureFood.AccountDomains;
using PureFood.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountRepository
{
    public interface IUserRoleRepository : ISqlDbBaseRepository<UserRole>
    {

    }
}
