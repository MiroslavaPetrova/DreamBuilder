using AutoMapper;
using DreamBuilder.Models.Orders.ViewModels;
using DreamBuilder.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DreamBuilder.Models.Invoices.ViewModels
{
    public class InvoicesMyViewModel : IMapFrom<Invoice>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public decimal Price { get; set; }

        public List<OrdersMyViewModel> Orders { get; set; }

        public int Products{ get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Invoice, InvoicesMyViewModel>()
                .ForMember(destination => destination.Price,   //TODO FIx it
                            opts => opts.MapFrom(origin => origin.Orders.Sum(order => order.Product.Price)));
                

        }
    }
}
