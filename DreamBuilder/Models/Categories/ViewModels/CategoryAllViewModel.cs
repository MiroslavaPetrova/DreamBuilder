using DreamBuilder.Services.Mapping;

namespace DreamBuilder.Models.Categories.ViewModels
{
    public class CategoryAllViewModel : IMapFrom<Category>
    {
        public string  Name { get; set; }
    }
}
