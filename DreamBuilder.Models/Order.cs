using System;
using System.Collections.Generic;

namespace DreamBuilder.Models
{
    public class Order
    {
        public Order()
        {
            this.Products = new List<ProductOrder>();
        }

        public string Id { get; set; }

        public DateTime CreationDate { get; set; }

        //public DateTime? EstimatedDeliveryDate { get; set; } ???

        public string CustomerId { get; set; }

        public DreamBuilderUser Customer { get; set; }

        public string InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public virtual ICollection<ProductOrder> Products { get; set; }
    }
}
