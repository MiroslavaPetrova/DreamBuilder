using System;

namespace DreamBuilder.Models
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string CustomerId { get; set; }

        public DreamBuilderUser Customer { get; set; }

        public string InvoiceId { get; set; }   //TODO: remove them => Invoice is automatically generated

        public Invoice Invoice { get; set; }

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
