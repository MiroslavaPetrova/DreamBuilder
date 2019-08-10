using DreamBuilder.Data;
using DreamBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DreamBuilder.Services.Tests
{
    public class OrdersServiceTests
    {
        //Testable IQueryable<Order> GetActiveOrders();
        //         CreateOrder(Order order)

        [Fact]
        public void GetActiveOrdersShouldReturnTheCountOfAllOrdersWithStatusActive()
        {
            var options = new DbContextOptionsBuilder<DreamBuilderDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;
            var dbContext = new DreamBuilderDbContext(options);

            var ordersService = new OrdersService(dbContext);

            OrderStatus orderStatus = new OrderStatus {Name = "Active" };
            Order order = new Order { Status = orderStatus };
            ordersService.CreateOrder(order);

            Assert.Equal(1, dbContext.Orders.Count());
        }

        [Fact]
        public void CreateOrderShouldAddAnOrder()
        {
            var options = new DbContextOptionsBuilder<DreamBuilderDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;
            var dbContext = new DreamBuilderDbContext(options);

            var ordersService = new OrdersService(dbContext);

            var order = new Order { Id = "123" };
            ordersService.CreateOrder(order);

            var orderId = dbContext.Orders.FirstOrDefault(o => o.Id == "123");

            Assert.Equal(order.Id, orderId.Id);
        }
    }
}
