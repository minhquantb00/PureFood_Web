using PureFood.CustomerApi.Shared.Models;
using PureFood.ProductReadModels;

namespace PureFood.CustomerApi.Mappings
{
    public static class ProductReviewMapping
    {
        public static ProductReviewModel ToModel(this RProductReview rProductReview)
        {
            return new ProductReviewModel
            {
                Comment = rProductReview.Comment,
                Id = rProductReview.Id,
                ProductId = rProductReview.ProductId,
                Rating = rProductReview.Rating,
                UserId = rProductReview.UserId,
            };
        }
    }
}
