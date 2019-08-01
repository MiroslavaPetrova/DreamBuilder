using DreamBuilder.Models;
using DreamBuilder.Models.Categories.InputModels;
using DreamBuilder.Models.Categories.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamBuilder.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var allCategories = this.categoriesService.GetAllCategories<CategoryAllViewModel>();

            return this.View(allCategories);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ProductCategoryCreateInputModel productCategoryCreateInputModel)
        {
            Category productCategory = AutoMapper.Mapper.Map<Category>(productCategoryCreateInputModel);

            this.categoriesService.AddProductCategory(productCategory);

            return this.RedirectToAction("All");
        }

        public IActionResult SearchByCategory(string search)
        {
            var categories = this.categoriesService.SarchByCategory(search);

            return this.View();
        }
    }
}
