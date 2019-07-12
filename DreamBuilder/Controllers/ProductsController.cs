using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamBuilder.Models;
using DreamBuilder.Models.Products.InputModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamBuilder.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
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
                Category = this.productsService.GetProductCategoryByName(inputModel.Category)
            };

            this.productsService.Create(product);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            return new JsonResult("I am fine");
        }
    }
}