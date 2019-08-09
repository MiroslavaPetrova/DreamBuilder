using DreamBuilder.Models.Invoices.ViewModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using DreamBuilder.Services.Mapping;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using DreamBuilder.Models;

namespace DreamBuilder.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult My()
        {
            // TODO:   Invoices/My/9ef37e07-d063-4fa5-8e55-a518cb86b814 -> compare currentUserId & the one in DB ?

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<InvoicesMyViewModel> myInvoices =
                this.invoiceService.GetAllByCustomerId(userId).To<InvoicesMyViewModel>().ToList();

            return this.View(myInvoices);


            //return new JsonResult("Hello");
        }
    }
}