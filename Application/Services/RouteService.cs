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
using Microsoft.EntityFrameworkCore;
using Application.Response.RouteStop;

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

                var driver = await _unitOfWork.Drivers.GetAsync(d => d.Id == request.DriverId);
                if (driver == null)
                {
                    return apiResponse.SetNotFound("Driver not found");
                }
                if (driver.Status == DriverStatus.OnRoute)
                {
                    return apiResponse.SetBadRequest("Driver is currently on delivery and cannot pick up orders");
                }
                if (driver.Status == DriverStatus.Inactive)
                {
                    return apiResponse.SetBadRequest("Driver has retired and cannot be assigned");
                }

                var invalidOrder = false;
                foreach (var stopRequest in routeStopRequests)
                {
                    var order = await _unitOfWork.Orders.GetAsync(o => o.Id == stopRequest.OrderId);
                    if (order == null)
                    {
                        invalidOrder = true;
                        break;
                    }
                    if (order.OrderStatus != OrderStatusEnum.Processing)
                    {
                        return apiResponse.SetBadRequest($"Order {order.Id} is not in 'Processing' status and cannot be included in a route.");
                    }
                }
                if (invalidOrder)
                {
                    return apiResponse.SetBadRequest("One or more orders do not exist.");
                }

                var route = _mapper.Map<Route>(request);
                route.RouteStatus = RouteStatus.Pending;
                route.CreateAt = DateTime.Now;

                await _unitOfWork.Routes.AddAsync(route);
                await _unitOfWork.SaveChangeAsync();

                route.RouteStops = new List<RouteStop>();

                foreach (var stopRequest in routeStopRequests)
                {
                    var routeStop = _mapper.Map<RouteStop>(stopRequest);
                    routeStop.RouteStatus = RouteStopStatus.Pending;
                    routeStop.RouteId = route.Id;
                    route.RouteStops.Add(routeStop);


                    var order = await _unitOfWork.Orders.GetAsync(o => o.Id == stopRequest.OrderId);
                    if (order != null)
                    {
                        order.OrderStatus = OrderStatusEnum.PendingPickUp;
                    }

                }

                await _unitOfWork.RouteStops.AddRangeAsync(route.RouteStops);

                driver.Status = DriverStatus.OnRoute;
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
                var routeStops = route?.RouteStops;
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
        
        public async Task<ApiResponse> GetAllRouteByDriverIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routes = await _unitOfWork.Routes.GetAllAsync(x => x.DriverId == id, include: x => x.Include(r => r.RouteStops));

                var responseList = _mapper.Map<List<RouteResponse>>(routes);
                return apiResponse.SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllRouteWithRouteStatusByDriverIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routes = await _unitOfWork.Routes.GetAllAsync(x => x.DriverId == id && (x.RouteStatus == RouteStatus.Pending || x.RouteStatus == RouteStatus.InProgress)
                , x => x.Include(r => r.RouteStops));

                if (routes.Count == 0)
                {
                    
                }

                var responseList = _mapper.Map<List<RouteResponse>>(routes);
                return apiResponse.SetOk(responseList);
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

        public async Task<ApiResponse> UpdateStopOrderAndStatusAsync(int RouteId)
        {
            try
            {
                var routeStops = await _unitOfWork.RouteStops.GetAllAsync(rs => rs.RouteId == RouteId);
                if (routeStops == null || !routeStops.Any()) return new ApiResponse().SetNotFound("RouteStops not found for the given RouteId");


                var route = await _unitOfWork.Routes.GetAsync(r => r.Id == RouteId);
                if (route == null) return new ApiResponse().SetNotFound("Route not found");
                //route.DeliveryStartDate = DateTime.Now;

                var orderIds = routeStops.Select(rs => rs.RouteId).Distinct();
                var orders = await _unitOfWork.Orders.GetAllAsync(o => orderIds.Contains(o.Id));
                if (orders == null) return new ApiResponse().SetNotFound("Orders not found for the given RouteId");

                var driverId = route.DriverId;
                var driver = await _unitOfWork.Drivers.GetAsync(d => d.Id == driverId);
                if (driver == null) return new ApiResponse().SetNotFound("Driver not found for the given Route");

                if (route.RouteStatus == RouteStatus.Pending)
                {
                    route.RouteStatus = RouteStatus.InProgress;
                    route.DeliveryStartDate = DateTime.Now;
                }

                bool hasCompletedStops = routeStops.Any(rs => rs.StopOrder == 0);
                bool hasPendingStops = false;

                foreach (var routeStop in routeStops.OrderBy(rs => rs.StopOrder))
                {
                    if (routeStop.StopOrder == 1)
                    {
                        routeStop.StopOrder = 0;
                        routeStop.RouteStatus = RouteStopStatus.Completed;
                    }
                    else if (routeStop.StopOrder > 1)
                    {
                        routeStop.StopOrder -= 1;
                        hasPendingStops = true;
                    }
                }

                if (hasPendingStops)
                {
                    if (route.RouteStatus == RouteStatus.Pending && !hasCompletedStops)
                    {
                        route.RouteStatus = RouteStatus.InProgress;
                        route.DeliveryStartDate = DateTime.Now;
                    }
                }
                else
                {
                    route.RouteStatus = RouteStatus.Completed;
                    driver.Status = DriverStatus.Available;
                }

                await _unitOfWork.SaveChangeAsync();

                var routeResponse = new RouteResponse()
                {
                    Id = RouteId,
                    Notes = route.Notes,
                    RouteStops = routeStops.Select(rs => new RouteStopResponse
                    {
                        Id = rs.Id,
                        StopOrder = rs.StopOrder,
                        RouteStopStatus = rs.RouteStatus
                    }).ToList(),
                };

                return new ApiResponse().SetOk("Update successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
