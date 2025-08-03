using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.EmailDomains
{
    public interface ICorrelationIdProvider
    {
        string Get();
    }
}
