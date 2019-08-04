using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using DreamBuilder.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DreamBuilder.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DreamBuilderDbContext context;

        public CategoriesService(DreamBuilderDbContext context)
        {
            this.context = context;
        }

        public Category GetProductCategoryByName(string categoryName)
        {
            Category productCategoryFromDb = this.context
                .Categories
                .SingleOrDefault(category => category.Name == categoryName);

            return productCategoryFromDb;
        }

        public IEnumerable<TViewModel> GetAllCategories<TViewModel>()
        {
            var allCategories = this.context.Categories.To<TViewModel>().ToList();
            return allCategories;
        }


        public void AddProductCategory(Category category)
        {
            this.context.Categories.Add(category);
            this.context.SaveChanges();
        }

        public IQueryable<Product> SarchByCategory(string search)
        {
            var productByCategory = this.context
                .Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name.Contains(search));

            return productByCategory;
        }
    }
}
