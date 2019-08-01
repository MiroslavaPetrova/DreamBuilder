using DreamBuilder.Models;
using System.Collections.Generic;

namespace DreamBuilder.Services.Contracts
{
    public interface ICategoriesService
    {
        Category GetProductCategoryByName(string name);

        IEnumerable<TViewModel> GetAllCategories<TViewModel>();

        void AddProductCategory(Category category);

        IEnumerable<Category> SarchByCategory(string search);
    }
}
