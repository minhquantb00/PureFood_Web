using PureFood.BaseDomains;
using PureFood.BaseReadModels;
using PureFood.CartCommands.Commands;
using PureFood.CartReadModels;
using System;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.CartDomains
{
    [Table("Cart_tbl")]
    public class Cart : BaseDomain
    {

        public Cart(CartAddCommand command) : base(command)
        {
            Id = command.Id;
            UserId = command.UserId;
        }

        public Cart(RCart model) : base(model)
        {
            Id = model.Id;
            UserId = model.UserId;
        }

        public new string Id { get; private set; }
        public string UserId { get; private set; }
    }
}
