using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamBuilder.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DreamBuilderDbContext context;

        public ProductsService(DreamBuilderDbContext context)
        {
            this.context = context;
        }

        public void Create(Product product)   //TODO: change this method
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public Category GetProductCategoryByName(string categoryName)
        {
            var productCategoryFromDb = this.context.Categories.FirstOrDefault(c => c.Name == categoryName);
            return productCategoryFromDb;
        }
    }
}
