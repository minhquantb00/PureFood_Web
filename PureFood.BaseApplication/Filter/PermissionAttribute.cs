using Microsoft.AspNetCore.Mvc;

namespace PureFood.BaseApplication.Filter
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string group, string name, bool isRoot, string key = "") : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { group, name, isRoot, key };
        }
    }
}
