using PureFood.ProductReadModels;
using PureFood.ServiceCMS.Shared.Models.Product;

namespace PureFood.ServiceCMS.Mappings
{
    public static class ProductImageMapping
    {
        public static ProductImageModel ToModel(this RProductImage productImage)
        {
            return new ProductImageModel
            {
                Id = productImage.Id,
                ProductId = productImage.ProductId,
                ImageUrl = productImage.ImageUrl,
                SortOrder = productImage.SortOrder,
                Title = productImage.Title
            };
        }
    }
}
