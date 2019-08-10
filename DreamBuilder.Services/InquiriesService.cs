using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Services.Contracts;
using System.Linq;

namespace DreamBuilder.Services
{
    public class InquiriesService : IInquiriesService
    {
        private readonly DreamBuilderDbContext context;

        public InquiriesService(DreamBuilderDbContext context)
        {
            this.context = context;
        }

        public void CreateInquiry(Inquiry inquiry)
        {
            inquiry.Status = this.context.InquiryStatuses.SingleOrDefault(inquiryStatus => inquiryStatus.Name == "Pending");

            this.context.Inquiries.Add(inquiry);
            this.context.SaveChanges();
        }
    }
}
