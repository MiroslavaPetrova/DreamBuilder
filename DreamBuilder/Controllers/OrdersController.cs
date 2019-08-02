using DreamBuilder.Models;
using DreamBuilder.Models.Orders.InputModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;


namespace DreamBuilder.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public OrdersController(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(OrdersCreateInputModel inputModel) // just to map productId
        {
            Order order = AutoMapper.Mapper.Map<Order>(inputModel);

            //using Microsoft.AspNetCore.Http;
            order.CustomerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            order.CreationDate = DateTime.UtcNow;
            this.ordersService.CreateOrder(order);

            return this.Redirect("/Products/All");
        }
    }
}