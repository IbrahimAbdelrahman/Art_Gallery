using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.AutoMapperProfiles
{
    public class DutchMappingProfiles:Profile
    {
        public DutchMappingProfiles()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(d => d.OrderId, ex => ex.MapFrom( s => s.OrderId))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemsViewModel>()
                .ReverseMap();
        }
    }
}
