using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("UserRole_tbl")]
    public class UserRole : BaseDomain
    {
        public UserRole(string userId, string roleId, StatusEnum status)
        {
            UserId = userId;
            RoleId = roleId;
            Status = status;
        }


        public string UserId { get; private set; }
        public string RoleId { get; private set; }
        public StatusEnum Status { get; private set; }
    }
}
