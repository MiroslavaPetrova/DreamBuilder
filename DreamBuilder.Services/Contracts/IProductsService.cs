using DreamBuilder.Models;
using System.Collections.Generic;

namespace DreamBuilder.Services.Contracts
{
    public interface IProductsService
    {
        void Create(Product product);

        IEnumerable<TViewModel> GetAllProducts<TViewModel>();
    }
}
