using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CustomerApi.Shared.Requests.Cart
{
    public record CartItemAddRequest
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public record CartItemChangeRequest : CartItemAddRequest
    {
        public string Id { get; set; }                                 
    }

    public record CartItemDeleteRequest
    {
        public string Id { get; set; }
    }

    public record CartItemGetByCartIdRequest
    {
        public string CartId {  set; get; }
    }

    public record CartItemGetByIdRequest
    {
        public string Id { get; set; }
    }
}
