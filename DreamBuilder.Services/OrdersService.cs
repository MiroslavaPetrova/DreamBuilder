using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;

namespace DreamBuilder.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DreamBuilderDbContext context;
        private readonly IProductsService productsService;
        private readonly UserManager<DreamBuilderUser> userManager;

        public OrdersService(DreamBuilderDbContext context, IProductsService productsService, UserManager<DreamBuilderUser> userManager)
        {
            this.context = context;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public void CreateOrder(Order order)
        {
            order.Status = this.context.OrderStatuses.SingleOrDefault(orderStatus => orderStatus.Name == "Active");

            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }
    }
}
