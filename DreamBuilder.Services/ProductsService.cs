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

        public ProductsService(DreamBuilderDbContext context)
        {
            this.context = context;
        }

        public void Create(Product product)   //TODO: think about changing this method
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }
      

        public IEnumerable<TViewModel> GetAllProducts<TViewModel>()
        {
            var allProducts = this.context.Products.To<TViewModel>().ToList();

            return allProducts;
        }
    }
}
