using PureFood.BaseDomains;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Events;
using PureFood.ProductReadModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductDomains
{
    [Table("ProductReview_tbl")]
    public class ProductReview : BaseDomain
    {
        public ProductReview(ProductReviewAddCommand command) : base(command)
        {
            Id = command.Id;
            ProductId = command.ProductId;
            UserId = command.UserId;
            Rating = command.Rating;
            Comment = command.Comment;
        }

        public ProductReview(RProductReview rProductReview) : base(rProductReview)
        {
            Id = rProductReview.Id;
            ProductId = rProductReview.ProductId;
            UserId = rProductReview.UserId;
            Rating = rProductReview.Rating;
            Comment = rProductReview.Comment;
        }

        public void Change(ProductReviewChangeCommand command)
        {
            Id = command.Id;
            ProductId = command.ProductId;
            UserId = command.UserId;
            Rating = command.Rating;
            Comment = command.Comment;
            Changed(command);
        }

        public ProductReviewAddEvent ToAddEvent()
        {
            return new ProductReviewAddEvent
            {
                ObjectId = Id
            };
        }

        public ProductReviewChangeEvent ToChangeEvent()
        {
            return new ProductReviewChangeEvent
            {
                ObjectId = Id,
            };
        }
        public new string Id { get; private set; }
        public string ProductId { get; private set; }
        public string UserId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
    }
}
