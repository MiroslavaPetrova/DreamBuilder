using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Models.Products.InputModels;
using DreamBuilder.Models.Products.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamBuilder.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly DreamBuilderDbContext context;

        public ProductsController(IProductsService productsService,
            ICategoriesService categoriesService, DreamBuilderDbContext context)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductCreateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            Product product = new Product
            {
                Name = inputModel.Name,
                Make = inputModel.Make,
                Model = inputModel.Model,
                Description = inputModel.Description,
                Image = inputModel.Image,
                Price = inputModel.Price,
                ManufacturedOn = inputModel.ManufacturedOn,
                Category = this.categoriesService.GetProductCategoryByName(inputModel.Category)
            };

            this.productsService.Create(product);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            List<ProductAllViewModel> AllProducts = productsService.GetAllProducts()
               .Select(products => new ProductAllViewModel
               {
                   Name = products.Name,
                   Make = products.Make,
                   Model = products.Model,
                   Description = products.Description,
                   Image = products.Image,
                   Price = products.Price,
                   ManufacturedOn = products.ManufacturedOn,
                   Category = products.Category.Name
               })
               .ToList();

            return this.View(AllProducts);
            //return new JsonResult("I am fine");
        }
    }
}