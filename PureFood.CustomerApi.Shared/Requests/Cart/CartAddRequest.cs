using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CustomerApi.Shared.Requests.Cart
{
    public record CartAddRequest
    {
        public string UserId { get; set; }
    }

    public record CartGetByUserIdRequest
    {
        public string UsertId { get; set; }
    }
}
