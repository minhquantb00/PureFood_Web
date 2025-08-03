using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ESRepositories
{
    public enum EsMethodName
    {
        // ReSharper disable once InconsistentNaming
        _search = 1,

        // ReSharper disable once InconsistentNaming
        _create = 2,

        // ReSharper disable once InconsistentNaming
        _bulk = 3,

        // ReSharper disable once InconsistentNaming
        _update = 4
    }
}
