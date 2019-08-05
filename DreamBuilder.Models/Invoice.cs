using System;
using System.Collections.Generic;

namespace DreamBuilder.Models
{
    public class Invoice
    {
        public Invoice()
        {
            this.Orders = new List<Order>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DreamBuilderUser Customer { get; set; }

        public DateTime IssuedOn { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
