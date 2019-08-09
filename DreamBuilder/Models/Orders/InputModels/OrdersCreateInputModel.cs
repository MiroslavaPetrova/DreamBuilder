using DreamBuilder.Services.Mapping;

namespace DreamBuilder.Models.Orders.InputModels
{
    public class OrdersCreateInputModel : IMapTo<Order>
    {
        public string ProductId { get; set; }

        //public int Quantity { get; set; }
    }
}
