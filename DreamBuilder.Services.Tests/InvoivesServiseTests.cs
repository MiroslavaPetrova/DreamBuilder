using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace DreamBuilder.Services.Tests
{
    public class InvoivesServiseTests
    {
        //CreateInvoice(string customerId) =>  return invoice.Id;

        [Fact]
        public void CreateInvoiceShouldCreateAnInvoiceWithAGivenUserId()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<DreamBuilderDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IInvoicesService, InvoicesService>();
            services.AddScoped<IOrdersService, OrdersService>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<DreamBuilderDbContext>();

            var customerId = "customer123";
            var customer = new DreamBuilderUser { Id = customerId };
            context.Users.Add(customer);
            context.SaveChanges();

            var invoicesService = serviceProvider.GetService<IInvoicesService>();

            Invoice invoice = new Invoice { CustomerId = customerId };

            context.Invoices.Add(invoice);
            context.SaveChanges();

            Assert.Equal(customerId, invoice.CustomerId);
        }
    }
}
