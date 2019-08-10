using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using System;
using System.Linq;

namespace DreamBuilder.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly DreamBuilderDbContext context;

        private readonly IOrdersService ordersService;

        public InvoicesService(DreamBuilderDbContext context, IOrdersService ordersService)
        {
            this.context = context;
            this.ordersService = ordersService;
        }

        public string CreateInvoice(string customerId)
        {
            Invoice invoice = new Invoice
            {
                CustomerId = customerId,
                IssuedOn = DateTime.UtcNow,
            };

            this.ordersService.FindOrdersOfAnInvoice(invoice);

            foreach (var order in invoice.Orders)
            {
                this.ordersService.Complete(order.Id);
            }

            this.context.Invoices.Add(invoice);
            this.context.SaveChanges();

            return invoice.Id;
        }

        public IQueryable<Invoice> GetAllByCustomerId(string customerId)
        {
            var myInvoices = this.context.Invoices.Where(invoice => invoice.CustomerId == customerId);

            return myInvoices;
        }
    }
}
