using PureFood.BaseDomains;
using PureFood.EnumDefine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.SystemDomains
{
    [Table("Language_tbl")] 
    public class Language : BaseDomain
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string? UniqueSeoCode { get; set; }
        public string? FlagImageFileName { get; set; }
        public byte Rtl { get; set; }
        public byte LimitedToStores { get; set; }
        public string? DefaultCurrencyId { get; set; }
        public StatusEnum Status { get; set; }
        public int DisplayOrder { get; set; }
        public string? DateFormat { get; set; }
        public string? NumberFormat { get; set; }
        public bool? IsDefault { get; set; }
    }
}
