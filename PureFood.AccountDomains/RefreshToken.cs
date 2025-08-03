using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AccountDomains
{
    [Table("RefreshToken_tbl")]
    public class RefreshToken : BaseDomain
    {
        public new string Id { get; private set; } = Guid.NewGuid().ToString("N");
        public string Token { get; private set; }
        public string UserId { get; private set; }
        public DateTime ExpiredTime { get; private set; }
    }
}
