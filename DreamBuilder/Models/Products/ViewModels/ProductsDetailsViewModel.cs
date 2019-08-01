using DreamBuilder.Services.Mapping;
using System;

namespace DreamBuilder.Models.Products.ViewModels
{
    public class ProductsDetailsViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string Model{ get; set; }

        public decimal Price { get; set; }

        public Category Category{ get; set; } // TODO Check if it maps

        public DateTime ManufacturedOn { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}
