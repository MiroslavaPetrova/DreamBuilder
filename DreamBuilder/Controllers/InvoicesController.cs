using DreamBuilder.Models.Invoices.ViewModels;
using DreamBuilder.Services.Contracts;
using DreamBuilder.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DreamBuilder.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesService invoicesService;

        public InvoicesController(IInvoicesService invoicesService)
        {
            this.invoicesService = invoicesService;
        }

        [HttpGet]
        public IActionResult My()
        {
            // TODO:   Invoices/My/9ef37e07-d063-4fa5-8e55-a518cb86b814 -> compare currentUserId & the one in DB ?

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<InvoicesMyViewModel> myInvoices =
                this.invoicesService.GetAllByCustomerId(userId).To<InvoicesMyViewModel>().ToList();

            return this.View(myInvoices);


            //return new JsonResult("Hello");
        }
    }
}