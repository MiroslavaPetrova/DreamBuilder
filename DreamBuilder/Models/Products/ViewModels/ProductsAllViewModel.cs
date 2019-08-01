using DreamBuilder.Services.Mapping;

namespace DreamBuilder.Models.Products.ViewModels
{
    public class ProductsAllViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}
