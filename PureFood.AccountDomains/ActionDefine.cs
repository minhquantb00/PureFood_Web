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
    [Table("ActionDefine_tbl")]
    public class ActionDefine : BaseDomain
    {
        public new string Id { get; private set; }
        public string? Name { get; private set; }
        public string Group { get; private set; }
        public bool IsRoot { get; private set; }
        public StatusEnum Status { get; private set; }
    }
}
