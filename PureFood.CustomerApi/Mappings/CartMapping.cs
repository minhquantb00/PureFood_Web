using PureFood.CartReadModels;
using PureFood.CustomerApi.Shared.Models;

namespace PureFood.CustomerApi.Mappings
{
    public static class CartMapping
    {
        public static CartModel ToModel(this RCart rCart)
        {
            if (rCart == null) return null!;
            return new CartModel
            {
                Id = rCart.Id,
                UserId = rCart.UserId,
            };
        }
    }
}
