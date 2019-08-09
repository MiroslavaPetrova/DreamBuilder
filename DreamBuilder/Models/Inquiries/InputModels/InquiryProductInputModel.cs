using DreamBuilder.Services.Mapping;

namespace DreamBuilder.Models.Inquiries.InputModels
{
    public class InquiryProductInputModel : IMapTo<Inquiry>
    {
        public string ProductId { get; set; }

        public string Content { get; set; }
    }
}
