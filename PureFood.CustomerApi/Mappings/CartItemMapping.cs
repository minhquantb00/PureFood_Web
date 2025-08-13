using PureFood.CartReadModels;
using PureFood.CustomerApi.Shared.Models;

namespace PureFood.CustomerApi.Mappings
{
    public static class CartItemMapping
    {
        public static CartItemModel ToModel(this RCartItem model)
        {
            return new CartItemModel
            {
                CartId = model.CartId,
                Id = model.Id,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
            };
        }
    }
}
