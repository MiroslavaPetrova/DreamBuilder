using DreamBuilder.Models;
using System.Collections.Generic;
using System.Linq;

namespace DreamBuilder.Services.Contracts
{
    public interface IProductsService
    {
        void Create(Product product);

        IQueryable<Product> GetAllProducts();
    }
}
