using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    [Table("Campaign_tbl")]
    public class Campaign : BaseDomain
    {
        public new string Id { get; private set; }
        public string Name { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
    }
}
