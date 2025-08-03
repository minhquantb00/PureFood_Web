using PureFood.AccountDomains;
using PureFood.AccountRepository;
using PureFood.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountRepositorySQLImplement
{
    public class UserRoleRepository(IDbConnectionFactory dbConnectionFactory): SqlDbBaseRepository<UserRole>(dbConnectionFactory),IUserRoleRepository, ISqlDbBaseRepository<UserRole>
    {
    }
}
