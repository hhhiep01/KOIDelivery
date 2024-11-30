using Application.Request.Driver;
using Application.Request.FishHealth;
using Application.Request.FishQualification;
using Application.Request.Order;
using Application.Request.Route;
using Application.Request.RouteStop;
using Application.Request.TransportService;
using Application.Response.Driver;
using Application.Response.RouteStop;
using Application.Response.TransportService;
using Application.Services;
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

            //TransportService
            CreateMap<TransportServiceRequest, TransportService>();
            CreateMap<TransportService, TransportServiceResponse>();

            //Route
            CreateMap<RouteRequest, Route>();

            //RouteStop
            CreateMap<RouteStopRequest, RouteStop>();
            CreateMap<RouteStopService, RouteStopResponse>();
            CreateMap<RouteStop, RouteStopResponse>();

            //Driver
            CreateMap<DriverRequest, Driver>();
            CreateMap<DriverService, DriverResponse>();

        }
    }
}
