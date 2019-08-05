using DreamBuilder.Models;
using DreamBuilder.Models.Orders.InputModels;
using DreamBuilder.Models.Orders.ViewModels;
using DreamBuilder.Services.Contracts;
using DreamBuilder.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DreamBuilder.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly IInvoiceService invoiceService;

        public OrdersController(IOrdersService ordersService, IProductsService productsService, IInvoiceService invoiceService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.invoiceService = invoiceService;
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

        [HttpPost]
        public IActionResult Complete()
        {
            //TODO: 
            // get the currently logged user Id and coplete the order => status == 2;
            // generate invoice for this specific order

            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string invoiceId = this.invoiceService.CreateInvoice(currentUserId);

            return this.Redirect("/Invoices/My"); //  /Invoives/My
        }
    }
}