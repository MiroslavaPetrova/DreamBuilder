using DreamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DreamBuilder.Services.Contracts
{
    public interface IOrdersService
    {
        void CreateOrder(Order order);
    }
}
