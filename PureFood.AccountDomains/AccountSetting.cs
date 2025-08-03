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
    [Table("AccountSetting_tbl")]
    public class AccountSetting : BaseDomain
    {
        public new string Id { get; private set; }
        public string UserId { get; private set; }
        public AccountSettingEnum Type { get; private set; }
        public string? Description { get; private set; }
        public string? Value { get; private set; }
        public StatusEnum Status { get; private set; }
    }
}
