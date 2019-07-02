using DreamBuilder.Models.Enums;

namespace DreamBuilder.Models
{
    public class Inquiry
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DreamBuilderUser User { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public RequiryStatus Status { get; set; }
    }
}
