using DreamBuilder.Models;

namespace DreamBuilder.Services.Contracts
{
    public interface IInquiryService
    {
        void CreateInquiry(Inquiry inquiry);
    }
}
