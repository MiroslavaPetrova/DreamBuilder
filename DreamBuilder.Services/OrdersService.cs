using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DreamBuilder.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly DreamBuilderDbContext context;

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

        public IQueryable<Order> GetActiveOrders()
        {
            var activeOrders = this.context.Orders
                .Include(order => order.Status)
                .Where(order => order.Status.Name == "Active");

            return activeOrders;
        }

        public void Complete(string orderId)
        {
            var activeOrder = this.context.Orders.SingleOrDefault(order => order.Id == orderId);

            //if (activeOrder == null || activeOrder.Status.Name != "Active")
            //{
            //    throw new ArgumentException(nameof(activeOrder));
            //}

            activeOrder.Status = this.context.OrderStatuses.SingleOrDefault(orderStatus => orderStatus.Name == "Completed");

            this.context.Update(activeOrder);
            this.context.SaveChanges();
        }

        public void FindOrdersOfAnInvoice(Invoice invoice)
        {
            List<Order> ordersOfAnInvoice =
                 this.context.Orders
                 .Where(order => order.CustomerId == invoice.CustomerId && order.Status.Name == "Active")
                 .ToList();

            invoice.Orders = ordersOfAnInvoice;
        }
    }
}
