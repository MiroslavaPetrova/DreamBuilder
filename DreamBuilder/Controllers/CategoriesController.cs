using DreamBuilder.Models;
using DreamBuilder.Models.Categories.InputModels;
using DreamBuilder.Models.Categories.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DreamBuilder.Services.Mapping;

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
        public IActionResult All(string search)     //TODO using DreamBuilder.Services.Mapping;   AUTOMAP!!!!!!!
        {
            List<ProductsSearchByCategoryViewModel> categories = this.categoriesService.SarchByCategory(search)
                .To<ProductsSearchByCategoryViewModel>()
                //.Select(category => new ProductsSearchByCategoryViewModel
                //{
                //    Name = category.Name,                              //TODO: test it and remove it
                //    Category = category.Category.Name,
                //    Image = category.Image
                //})
                .ToList();

            return this.View(categories);
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

            return this.Redirect("/Products/Create");  
        }
    }
}
