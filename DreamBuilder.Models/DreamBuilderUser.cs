using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DreamBuilder.Models
{
    public class DreamBuilderUser : IdentityUser
    {
        public DreamBuilderUser()
        {
            this.Orders = new List<Order>();

            this.Inquiries = new List<Inquiry>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}
