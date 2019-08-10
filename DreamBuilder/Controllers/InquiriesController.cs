using DreamBuilder.Models;
using DreamBuilder.Models.Inquiries.InputModels;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace DreamBuilder.Controllers
{
    public class InquiriesController : Controller
    {
        private readonly IInquiriesService inquiriesService;

        public InquiriesController(IInquiriesService inquiriesService)
        {
            this.inquiriesService = inquiriesService;
        }

        [HttpPost]
        public IActionResult MakeInquiry(InquiryProductInputModel inputModel)
        {
            Inquiry inquiry = AutoMapper.Mapper.Map<Inquiry>(inputModel);

            inquiry.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            inquiry.PlacedOn = DateTime.UtcNow;
            this.inquiriesService.CreateInquiry(inquiry);

            //return this.Redirect("/Inquiries/My"); 

            return new JsonResult("Thank you for your inquiry! A reply will be sent to you as soon as possible.");
        }
    }
}