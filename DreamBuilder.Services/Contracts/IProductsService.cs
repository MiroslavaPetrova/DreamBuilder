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

        //IQueryable<Product> All();
    }
}
