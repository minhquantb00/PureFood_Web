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
    [Table("ProductImage_tbl")]
    public class ProductImage : BaseDomain
    {
        public ProductImage(ProductImageAddCommand command)
            : base(command)
        {
            Id = command.Id;
            Title = command.Title;
            ProductId = command.ProductId;
            ImageUrl = command.ImageUrl;
            SortOrder++;
        }
        public ProductImage(RProductImage rProductImage)
            : base(rProductImage)
        {
            Id = rProductImage.Id;
            Title = rProductImage.Title;
            ProductId = rProductImage.ProductId;
            ImageUrl = rProductImage.ImageUrl;
            SortOrder = rProductImage.SortOrder;
        }

        public void Change(ProductImageChangeCommand command)
        {
            Id = command.Id;
            Title = command.Title;
            ProductId = command.ProductId;
            ImageUrl = command.ImageUrl;
        }

        public ProductImageChangeEvent ToChangeEvent()
        {
            return new ProductImageChangeEvent
            {
                ObjectId = Id,
            };
        }
        public ProductImageAddEvent ToAddEvent()
        {
            return new ProductImageAddEvent
            {
                ObjectId = Id,
            };
        }
        public new string Id { get; private set; }
        public string Title { get; private set; }
        public string ProductId { get; private set; }
        public string ImageUrl { get; private set; }
        public int SortOrder { get; private set; }
    }
}
