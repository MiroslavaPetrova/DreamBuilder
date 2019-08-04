using DreamBuilder.Models;
using DreamBuilder.Models.Orders.InputModels;
using DreamBuilder.Models.Orders.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DreamBuilder.Services.Mapping;
using DreamBuilder.Data;
using Microsoft.EntityFrameworkCore;

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

        //public IActionResult Create()
        //{
        //    return this.View();
        //}

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

        [HttpGet]
        public IActionResult My()
        {

            List<OrdersMyViewModel> myOrders = this.ordersService.GetActiveOrders()
            .Where(order => order.CustomerId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            .To<OrdersMyViewModel>()
            .ToList();

            //.Select(order => new OrdersMyViewModel
            //{
            //    CreationDate = DateTime.UtcNow,
            //    ProductImage = order.Product.Image,
            //    ProductName = order.Product.Name,
            //    ProductPrice = order.Product.Price,
            //    Id = order.Id,
            //})
            //.ToList();

            return this.View(myOrders);
        }
        [HttpGet]
        public IActionResult Complete()
        {
            return null;
        }
    }
}