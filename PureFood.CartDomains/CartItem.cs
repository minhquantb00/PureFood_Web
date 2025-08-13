using PureFood.BaseDomains;
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
    [Table("CartItem_tbl")] 
    public class CartItem : BaseDomain
    {
        public CartItem(CartItemAddCommand command) : base(command)
        {
            Id = command.Id;
            CartId = command.CartId;
            ProductId = command.ProductId;
            Quantity = command.Quantity;
        }
        public CartItem(RCartItem model) : base(model)
        {
            Id = model.Id;
            CartId = model.CartId;
            ProductId = model.ProductId;
            Quantity = model.Quantity;
        }

        public void Change(CartItemChangeCommand command)
        {
            CartId = command.CartId;
            ProductId = command.ProductId;
            Quantity = command.Quantity;
            Changed(command);
        }
        public new string Id { get; private set; }
        public string CartId { get; private set; }
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }
    }
}
