using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Application.Response;
using Microsoft.AspNetCore.Localization.Routing;
using Application.Request.Route;
using Application.Request.RouteStop;
using Application.Response.Route;
using Application.Interface;

namespace Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> AddRouteAsync(RouteRequest request, List<RouteStopRequest> routeStopRequests)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                if (routeStopRequests == null || !routeStopRequests.Any())
                {
                    return apiResponse.SetBadRequest("RouteStopRequests is empty or null");
                }

                var route = _mapper.Map<Route>(request);
                route.RouteStatus = RouteStatus.Pending;
                route.CreateAt = DateTime.Now;

                await _unitOfWork.Routes.AddAsync(route);
                await _unitOfWork.SaveChangeAsync();
                Console.WriteLine($"Route Id after SaveChange: {route.Id}");

                route.RouteStops = new List<RouteStop>();

                foreach (var stopRequest in routeStopRequests)
                {
                    var routeStop = _mapper.Map<RouteStop>(stopRequest);
                    routeStop.RouteStatus = RouteStopStatus.Pending;
                    routeStop.RouteId = route.Id;
                    route.RouteStops.Add(routeStop);
                }
                await _unitOfWork.RouteStops.AddRangeAsync(route.RouteStops);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk("Add Success");
            }
            catch (Exception ex) 
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllRouteAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routes = await _unitOfWork.Routes.GetAllAsync(null);
                var responseList = _mapper.Map<List<RouteResponse>>(routes);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetRouteByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var route = await _unitOfWork.Routes.GetAsync(x => x.Id == id);
                if (route is null)
                {
                    return apiResponse.SetBadRequest("Can not found Route Id : " + id);
                }
                var response = _mapper.Map<RouteResponse>(route);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteRouteByIdAsync(int id)
        {
            try
            {
                var route = await _unitOfWork.Routes.GetAsync(x => x.Id == id);
                if (route == null)
                    return new ApiResponse().SetNotFound("Route not found");
                await _unitOfWork.Routes.RemoveByIdAsync(route.Id);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("Route deleted successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
