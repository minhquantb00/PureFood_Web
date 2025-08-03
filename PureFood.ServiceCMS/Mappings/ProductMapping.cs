using PureFood.ProductReadModels;
using PureFood.ServiceCMS.Shared.Models.Product;

namespace PureFood.ServiceCMS.Mappings
{
    public static class ProductMapping
    {
        public static ProductModel ToModel(RProduct rProduct)
        {
            return new ProductModel
            {
                Id = rProduct.Id,
                ProductTypeId = rProduct.ProductTypeId,
                Name = rProduct.Name,
                Price = rProduct.Price,
                AvatarImage = rProduct.AvatarImage,
                Title = rProduct.Title,
                Discount = rProduct.Discount,
                PriceDiscount = rProduct.PriceDiscount,
                Quantity = rProduct.Quantity,
                NumberOfSales = rProduct.NumberOfSales,
                Description = rProduct.Description,
                ShortDescription = rProduct.ShortDescription,
                Status = rProduct.Status
            };
        }
    }
}
