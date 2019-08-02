using DreamBuilder.Models;
using System.Collections.Generic;
using System.Linq;

namespace DreamBuilder.Services.Contracts
{
    public interface ICategoriesService
    {
        Category GetProductCategoryByName(string name);

        IEnumerable<TViewModel> GetAllCategories<TViewModel>();

        void AddProductCategory(Category category);

        IQueryable<Product> SarchByCategory(string search);
    }
}
