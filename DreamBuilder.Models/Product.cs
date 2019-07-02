using System;
using System.Collections.Generic;

namespace DreamBuilder.Models
{
    public class Product
    {
        public Product()
        {
            this.Inquiries = new List<Inquiry>();

            this.Orders = new List<ProductOrder>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }

        public virtual ICollection<ProductOrder> Orders { get; set; }
    }
}
