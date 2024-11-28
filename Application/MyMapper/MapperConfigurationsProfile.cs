using Application.Request.FishHealth;
using Application.Request.FishQualification;
using Application.Request.Order;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MyMapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<OrderRequest, Order>();
            CreateMap<OrderFishRequest, OrderFish>();
            CreateMap<FishHealthRequest, FishHealth>();
            CreateMap<FishQualificationRequest, FishQualification>();
        }
    }
}
