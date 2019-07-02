using System;

namespace DreamBuilder.Models
{
    public class Invoice
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DreamBuilderUser Customer { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        public DateTime IssuedOn { get; set; }
    }
}
