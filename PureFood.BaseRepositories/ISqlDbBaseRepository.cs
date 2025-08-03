using PureFood.BaseDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.BaseRepositories
{
    public interface ISqlDbBaseRepository<in T> where T : BaseDomain
    {
        Task Add(T obj);
        Task Change(T obj);
        Task Remove(T obj);
        Task RemoveRange(IEnumerable<T> obj);
    }
}
