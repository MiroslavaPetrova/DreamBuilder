using DreamBuilder.Services.Mapping;

namespace DreamBuilder.Models.Categories.InputModels
{
    public class ProductCategoryCreateInputModel : IMapTo<Category>
    {
        public string Name { get; set; }
    }
}
