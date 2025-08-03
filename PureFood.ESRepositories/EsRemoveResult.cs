using PureFood.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ESRepositories
{
    public class EsRemoveResult
    {
        public string? result { get; set; }
        public bool errors => result?.AsEmpty() != "deleted";
    }
}
