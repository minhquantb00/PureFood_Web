using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Table("EmailAccountSettings_tbl")]
    public class EmailAccountSettings : BaseDomain
    {
        public new string Id { get; private set; }
        public string DefaultEmailAccountId { get; private set; }
        public string PickupDirectoryLocation { get; private set; }
    }
}
