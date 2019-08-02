using DreamBuilder.Models;
using DreamBuilder.Models.Categories.InputModels;
using DreamBuilder.Models.Categories.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult All(string search)         // remove it or add the search functionality in here
        {
            List<ProductsSearchByCategoryViewModel> categories = this.categoriesService.SarchByCategory(search)
                .Select(cat => new ProductsSearchByCategoryViewModel
                {
                    Name = cat.Name,
                    Category = cat.Category.Name,
                    Image = cat.Image
                })
                .ToList();


            return this.View(categories);
            //var allCategories = this.categoriesService.GetAllCategories<CategoryAllViewModel>();

            //return this.View(allCategories);
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

            return this.Redirect("/Products/Create");   //TODO change redirect to /product/create
        }

        //public IActionResult SearchByCategory(string search)
        //{
        //    List<ProductsSearchByCategoryViewModel> categories = this.categoriesService.SarchByCategory(search)
        //        .Select(cat => new ProductsSearchByCategoryViewModel
        //        {
        //            Name = cat.Name,
        //            Category = cat.Category.Name,
        //            Image = cat.Image
        //        })
        //        .ToList();


        //    return this.View(categories);
        //}
    }
}
