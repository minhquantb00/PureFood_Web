using PureFood.BaseDomains;
using PureFood.EnumDefine;
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
    [Table("Product_tbl")]
    public class Product : BaseDomain
    {
        public Product(RProduct rProduct) : base(rProduct)
        {
            Id = rProduct.Id;
            ProductTypeId = rProduct.ProductTypeId;
            Name = rProduct.Name;
            Price = rProduct.Price;
            AvatarImage = rProduct.AvatarImage;
            Title = rProduct.Title;
            Status = rProduct.Status;
            Discount = rProduct.Discount;
            PriceDiscount = rProduct.PriceDiscount;
            Quantity = rProduct.Quantity;
            NumberOfSales = rProduct.NumberOfSales;
            Description = rProduct.Description;
            ShortDescription = rProduct.ShortDescription;
        }


        public Product(ProductAddCommand command) : base(command)
        {
            Id = command.Id;
            ProductTypeId = command.ProductTypeId;
            Name = command.Name;
            Price = command.Price;
            Status = command.Status;
            AvatarImage = command.AvatarImage;
            Title = command.Title;
            Discount = command.Discount;
            PriceDiscount = command.PriceDiscount;
            Quantity = command.Quantity;
            NumberOfSales = 0;
            Description = command.Description;
            ShortDescription = command.ShortDescription;
        }
        public void Change(ProductChangeCommand command)
        {
            Id = command.Id;
            ProductTypeId = command.ProductTypeId;
            Name = command.Name;
            Price = command.Price;
            AvatarImage = command.AvatarImage;
            Status = command.Status;
            Title = command.Title;
            Discount = command.Discount;
            PriceDiscount = command.PriceDiscount;
            Quantity = command.Quantity;
            Description = command.Description;
            ShortDescription = command.ShortDescription;
            Changed(command);
        }

        public void Delete(ProductDeleteCommand command)
        {
            StatusEnum status = StatusEnum.Deleted;
            Changed(command);
        }
        public ProductAddEvent ToAddEvent()
        {
            return new ProductAddEvent
            {
                ObjectId = Id,
            };
        }

        public ProductChangeEvent ToChangeEvent()
        {
            return new ProductChangeEvent
            {
                ObjectId = Id,
            };
        }
        public new string Id { get; private set; }
        public string ProductTypeId { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string AvatarImage { get; private set; }
        public string Title { get; private set; }
        public int Discount { get; private set; }
        public double PriceDiscount { get; private set; }
        public int Quantity { get; private set; }
        public int NumberOfSales { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public StatusEnum Status { get; private set; }
    }
}
