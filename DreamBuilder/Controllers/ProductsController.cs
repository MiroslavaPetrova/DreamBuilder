using DreamBuilder.Models;
using DreamBuilder.Models.Categories.ViewModels;
using DreamBuilder.Models.Products.InputModels;
using DreamBuilder.Models.Products.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var allCategories = this.categoriesService.GetAllCategories<CategoryAllViewModel>();

            this.ViewData["categories"] = allCategories
                                          .Select(productCategory => new CategoryCreateProductCategoryViewModel
                                          { Name = productCategory.Name })
                                          .ToList();
            //TODO Seed some makes & models in the DB & make them w/ select option when creating the product
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Create(ProductsCreateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

           //Category categoryFromDb = this.categoriesService.GetProductCategoryByName(inputModel.Category);

            //TODO try implement the AutoMapper here
            //Product product = AutoMapper.Mapper.Map<Product>(inputModel);
            //product.Category = this.categoriesService.GetProductCategoryByName(inputModel.Category);

            Product product = new Product // TODO MOVE IT TO THE SERVICES. DO NOT EXPOSE THE ENTITY!!! => MAPPING FAILED
            {
                Id = inputModel.Id,
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

        [HttpGet]
        public IActionResult All()
        {
            var allProducts = this.productsService.GetAllProducts<ProductsAllViewModel>();

            return this.View(allProducts);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            Product product = this.productsService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductsDetailsViewModel productDetailsViewModel
               = AutoMapper.Mapper.Map<ProductsDetailsViewModel>(product);

            return this.View(productDetailsViewModel); 
        }
    }
}