using DreamBuilder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DreamBuilder.Data
{                                                         
    public class DreamBuilderDbContext : IdentityDbContext<DreamBuilderUser, IdentityRole, string>
    {
        public DreamBuilderDbContext(DbContextOptions<DreamBuilderDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Inquiry> Inquiries { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //    builder.Entity<ProductOrder>()
            //        .HasKey(po => new { po.ProductId, po.OrderId });

            //builder.Entity<Order>()
            //    .HasOne(order => order.Invoice)
            //    .WithOne(invoice => invoice.Order)
            //    .HasForeignKey<Invoice>(order => order.OrderId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
