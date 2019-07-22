using DreamBuilder.Models;
using DreamBuilder.Models.Categories.ViewModels;
using DreamBuilder.Models.Products.InputModels;
using DreamBuilder.Models.Products.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace DreamBuilder.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProductsController(IProductsService productsService,
            ICategoriesService categoriesService, IHostingEnvironment hostingEnvironment)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.hostingEnvironment = hostingEnvironment;
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
            if (inputModel.Image.Name.EndsWith("jpg") || inputModel.Image.Name.EndsWith("png"))
            {
                var fileName = this.hostingEnvironment.WebRootPath + "\\files-pictures\\";

                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    inputModel.Image.CopyTo(fileStream);
                }
            }
            var imagePath = "\\file-pictures\\" + product.Id;
           
            product.Image = imagePath;

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