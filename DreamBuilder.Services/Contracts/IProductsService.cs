using DreamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamBuilder.Services.Contracts
{
    public interface IProductsService
    {
        void Create(Product product);

        Category GetProductCategoryByName(string name); // Move it to categoryServices OBLOGATORY!!!

        //IQueryable<Product> All();
    }
}
