using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DreamBuilder.Services.Tests
{
    public class ProductsServiceTests
    {
        //Testable:
        //  GetById(string id);
        //  CreateProduct();

        [Fact]
        public void GetByIdShouldReturnProductWithCertainProductId()    
        {
            var services = new ServiceCollection();

            services.AddDbContext<DreamBuilderDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<DreamBuilderDbContext>();

            var productId = "456";

            var products = new List<Product>
            {
                new Product { Id = "233", Name = "Mini"},
                new Product { Id = "123", Name = "BackHoeLoader"},
                new Product { Id = productId, Name = "Excavator" },
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            var productsService = serviceProvider.GetService<IProductsService>();

            Product product = productsService.GetById(productId);

            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public void CreateProductShouldAddProduct()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DreamBuilderDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<DreamBuilderDbContext>();

            Product product = new Product { Name = "Excavator" };
            context.Products.Add(product);
            context.SaveChanges();

            var actualProduct = context.Products.ToList().FirstOrDefault(p => p.Name == product.Name);

            Assert.Equal(product.Name, actualProduct.Name);
        }
      
    }
}
