using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace DreamBuilder.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DreamBuilderDbContext context;
        private readonly UserManager<DreamBuilderUser> userManager;

        public OrdersService(DreamBuilderDbContext context)
        {
            this.context = context;
        }

        public void CreateOrder(Order order)
        {
            order.Status = this.context.OrderStatuses.SingleOrDefault(orderStatus => orderStatus.Name == "Active");

            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }
    }
}
