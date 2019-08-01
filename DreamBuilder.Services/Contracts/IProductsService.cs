using DreamBuilder.Models;
using System.Collections.Generic;

namespace DreamBuilder.Services.Contracts
{
    public interface IProductsService
    {
        void Add(Product product);

        IEnumerable<TViewModel> GetAllProducts<TViewModel>();

        Product GetById(string id);

    }
}
