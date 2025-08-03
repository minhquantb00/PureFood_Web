using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("ConfirmEmail_tbl")]
    public class ConfirmEmail : BaseDomain
    {
        public ConfirmEmail(string userId, string confirmCode, DateTime expiredTime, bool isConfirmed = false) : base()
        {
            UserId = userId;
            ConfirmCode = confirmCode;
            ExpiredTime = expiredTime;
            IsConfirmed = isConfirmed;
        }
        public string ConfirmCode { get; private set; }
        public string UserId { get; private set; }
        public DateTime ExpiredTime { get; private set; }
        public bool IsConfirmed { get; private set; }
    }
}
