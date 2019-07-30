using System.Collections.Generic;
using System.Linq;
using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using DreamBuilder.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DreamBuilder.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DreamBuilderDbContext context;
        private readonly ICategoriesService categoriesService;

        public ProductsService(DreamBuilderDbContext context, ICategoriesService categoriesService)
        {
            this.context = context;
            this.categoriesService = categoriesService;
        }

        public void Create(Product product)   //TODO: think about changing this method
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }


        public IEnumerable<TViewModel> GetAllProducts<TViewModel>()
        {
            var allProducts = this.context
                .Products
                .To<TViewModel>()
                .ToList();

            return allProducts;
        }

        public Product GetById(string id)
        {
            Product productFromDb = this.context.Products
                .Where(product => product.Id == id)
                .Include(product => product.Category)
                .SingleOrDefault();

            return productFromDb;

            //TODO Need to map more than the ID

            //var productId = this.context
            //    .Products
            //    .SingleOrDefault(product => product.Id == id);

            //return productId;
        }
    }
}
