using Application.Request.Fish;
using Application.Request.FishHealth;
using Application.Request.Order;
using Application.Request.TransportService;
using Application.Response.Fish;
using Application.Response.Order;
using Application.Response.TransportService;
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
            CreateMap<Order, OrderResponse>();

            CreateMap<OrderFishRequest, OrderFish>();
            CreateMap<OrderFish, OrderFishResponse>();

            CreateMap<FishHealthRequest, FishHealth>();
            CreateMap<FishHealth, FishHealthResponse>();

            CreateMap<FishQualificationRequest, FishQualification>();
            CreateMap<FishQualification, FishQualificationResponse>();

            //TransportService
            CreateMap<TransportServiceRequest, TransportService>();
            CreateMap<TransportService, TransportServiceResponse>();

            

        }
    }
}
