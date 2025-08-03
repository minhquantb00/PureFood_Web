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
    [Table("ProductType_tbl")]  
    public class ProductType : BaseDomain
    {
        public ProductType(ProductTypeAddCommand command) : base(command)
        {
            Id = command.Id;
            Name = command.Name;
            ImageUrl = command.ImageUrl;
        }
        public void Change(ProductTypeChangeCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command), "Command cannot be null");
            if (command.Id != Id) throw new InvalidOperationException("Command ID does not match the current ProductType ID");
            Name = command.Name;
            ImageUrl = command.ImageUrl;
            Changed(command);
        }

        public ProductType(RProductType model) : base(model)
        {
            Id = model.Id;
            Name = model.Name;
            ImageUrl = model.ImageUrl;
        }

        public ProductTypeAddEvent ToAddEvent()
        {
            return new ProductTypeAddEvent
            {
                ObjectId = Id,
            };
        }

        public ProductTypeChangeEvent ToChangeEvent()
        {
            return new ProductTypeChangeEvent
            {
                ObjectId = Id,
            };
        }
        public new string Id { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
    }
}
