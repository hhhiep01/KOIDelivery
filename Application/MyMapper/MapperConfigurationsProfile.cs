using Application.Request.Driver;
using Application.Request.Fish;
using Application.Request.FishHealth;
using Application.Request.Order;
using Application.Request.Route;
using Application.Request.RouteStop;
using Application.Request.TransportService;
using Application.Response.Driver;
using Application.Response.RouteStop;
using Application.Response.Fish;
using Application.Response.Order;
using Application.Response.Order;
using Application.Response.TransportService;
using Application.Services;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Request.Feedback;
using Application.Response.Feedback;
using Application.Response.UserAccount;
using Application.Response.Route;
using Application.Request.Feedback;
using Application.Response.Feedback;
using Application.Request.User;
using Application.Request.KoiSize;
using Application.Response.KoiSize;
using Application.Request.BoxType;
using Application.Response.BoxType;
using Application.Request.FishDetail;
using Application.Response.FishDetail;
using Application.Response.OrderItem;
using Application.Response.BoxAllocation;

namespace Application.MyMapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<OrderRequest, Order>();
            CreateMap<Order, OrderResponse>();

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.TransportService, opt => opt.MapFrom(src => src.TransportService))
                //.ForMember(dest => dest.OrderFishes, opt => opt.MapFrom(src => src.OrderFishs))
                .ForMember(dest => dest.RouteStops, opt => opt.MapFrom(src => src.RouteStops)); 
           /* CreateMap<OrderFishRequest, OrderFish>();
            CreateMap<OrderFish, OrderFishResponse>()
                 .ForMember(dest => dest.FishQualifications, opt => opt.MapFrom(src => src.FishQualifications))
                 .ForMember(dest => dest.FishHealths, opt => opt.MapFrom(src => src.FishHealths));*/
            CreateMap<FishHealthRequest, FishHealth>();
            CreateMap<FishHealth, FishHealthResponse>();

            CreateMap<FishQualificationRequest, FishQualification>();
            CreateMap<FishQualification, FishQualificationResponse>();

            //TransportService
            CreateMap<TransportServiceRequest, TransportService>();
            CreateMap<TransportService, TransportServiceResponse>();
            CreateMap<TransportLocalServiceRequest, TransportService>();
            CreateMap<TransportService, TransportLocalServiceResponse>();

                 

            //Route
            CreateMap<RouteRequest, Route>();
            CreateMap<Route, RouteResponse>();

            //RouteStop
            CreateMap<RouteStopRequest, RouteStop>();
            CreateMap<RouteStopService, RouteStopResponse>();
            CreateMap<RouteStop, RouteStopResponse>();

            //Driver
            CreateMap<DriverRequest, Driver>();
            CreateMap<DriverService, DriverResponse>();
            CreateMap<Driver, DriverResponse>()
                    .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(src => src.UserAccount));

            CreateMap<FeedbackRequest, Order>();
            CreateMap<Order, FeedbackResponse>();

            //UserAccount
            //CreateMap<UserProfileResponse, UserAccount>();
            CreateMap<UpdateUserRequest, UserAccount>();
            CreateMap<UserAccount, UserProfileResponse>();
            CreateMap<UserAccount, AccountResponse>();

            //KoiSize
            CreateMap<KoiSizeUpdateRequest, KoiSize>();
            CreateMap<KoiSizeRequest, KoiSize>();
            CreateMap<KoiSize, KoiSizeResponse>();
             


            //BoxSize
            CreateMap<BoxTypeUpdateRequest, BoxType>();
            CreateMap<BoxTypeRequest, BoxType>();
            CreateMap<BoxType, BoxTypeResponse>();

            //FishDetail
            CreateMap<FishDetailUpdateRequest, FishDetail>();
            CreateMap<FishDetailRequest, FishDetail>();
            CreateMap<FishDetail, FishDetailResponse>();

            //OrderItem
            CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.KoiSize, opt => opt.MapFrom(src => src.KoiSize));
            //
            CreateMap<BoxAllocation, BoxAllocationResponse>()
           .ForMember(dest => dest.BoxType, opt => opt.MapFrom(src => src.BoxType));
        }
    }
}
