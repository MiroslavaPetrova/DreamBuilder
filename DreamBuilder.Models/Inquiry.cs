using System;

namespace DreamBuilder.Models
{
    public class Inquiry
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime PlacedOn { get; set; } // add this after dropping the DB

        public string UserId { get; set; }

        public DreamBuilderUser User { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public InquiryStatus Status { get; set; }
    }
}
