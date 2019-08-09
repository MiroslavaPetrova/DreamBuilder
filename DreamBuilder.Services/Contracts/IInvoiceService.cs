using DreamBuilder.Models;
using System.Linq;

namespace DreamBuilder.Services.Contracts
{
    public interface IInvoiceService
    {
        string CreateInvoice(string customerId);

        IQueryable<Invoice> GetAllByCustomerId(string customerId);
    }
}
