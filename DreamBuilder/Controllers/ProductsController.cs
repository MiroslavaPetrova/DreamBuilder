using DreamBuilder.Models;
using DreamBuilder.Models.Categories.ViewModels;
using DreamBuilder.Models.Products.InputModels;
using DreamBuilder.Models.Products.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DreamBuilder.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(IProductsService productsService,
            ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var allCategories = this.categoriesService.GetAllCategories<CategoryAllViewModel>();

            this.ViewData["categories"] = allCategories
                                          .Select(productCategory => new CategoryCreateProductCategoryViewModel
                                          { Name = productCategory.Name })
                                          .ToList();

            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
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
                Image = inputModel.Image.FileName, 
                Price = inputModel.Price,
                ManufacturedOn = inputModel.ManufacturedOn,
                Category = this.categoriesService.GetProductCategoryByName(inputModel.Category)
            };

            this.productsService.Create(product);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var allProducts = this.productsService.GetAllProducts<ProductAllViewModel>();

            return this.View(allProducts);
        }
    }
}