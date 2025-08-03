using PureFood.ProductReadModels;
using PureFood.ServiceCMS.Shared.Models.Product;

namespace PureFood.ServiceCMS.Mappings
{
    public static class ProductTypeMapping
    {
        public static ProductTypeModel ToModel(this RProductType productType)
        {
            return new ProductTypeModel
            {
                Id = productType.Id,
                Name = productType.Name,
                ImageUrl = productType.ImageUrl
            };
        }
    }
}
