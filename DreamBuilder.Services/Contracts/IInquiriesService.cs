using DreamBuilder.Models;

namespace DreamBuilder.Services.Contracts
{
    public interface IInquiriesService
    {
        void CreateInquiry(Inquiry inquiry);
    }
}
