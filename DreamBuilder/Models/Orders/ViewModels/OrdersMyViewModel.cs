using DreamBuilder.Services.Mapping;
using System;

namespace DreamBuilder.Models.Orders.ViewModels
{
    public class OrdersMyViewModel : IMapFrom<Order>, IMapTo<Order>
    {
        public string Id { get; set; }

        public string ProductImage { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
