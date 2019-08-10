using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;



namespace DreamBuilder.Services.Tests
{
    public class InquiriesServiceTests
    {
        //CreateInquiry(Inquiry inquiry);

        [Fact]
        public void CreateInquiryShouldInquiryWithStatusPending()
        {
            var options = new DbContextOptionsBuilder<DreamBuilderDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var dbContext = new DreamBuilderDbContext(options);

            var inquiryService = new InquiriesService(dbContext);

            InquiryStatus inquiryStatus = new InquiryStatus { Name = "Pending" };
            var inquiry = new Inquiry { Status = inquiryStatus };
            
            inquiryService.CreateInquiry(inquiry);

            var pendingInquiries = dbContext.Inquiries.Where(i => i.Status.Name == "Pending");

            Assert.Equal(1, dbContext.Inquiries.Count());
        }
    }
}
