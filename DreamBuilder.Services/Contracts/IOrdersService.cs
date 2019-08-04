﻿using DreamBuilder.Models;
using System.Linq;

namespace DreamBuilder.Services.Contracts
{
    public interface IOrdersService
    {
        void CreateOrder(Order order);

        IQueryable<Order> GetActiveOrders();
    }
}
