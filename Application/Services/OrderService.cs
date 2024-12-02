﻿using Application.Interface;
using Application.Request.Feedback;
using Application.Request.Order;
using Application.Response;
using Application.Response.Order;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IClaimService _claim;
        private readonly IGoogleMapService _googleMapService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claim, IGoogleMapService googleMapService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claim = claim;
            _googleMapService = googleMapService;
        }

        public async Task<ApiResponse> CreateOrderAsync(OrderRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var claim = _claim.GetUserClaim();
                var transportService = await _unitOfWork.TransportServices.GetAsync(x => x.Id == request.TransportServiceId);
                if (transportService is null)
                {
                    return new ApiResponse().SetNotFound("TransportService not found");
                }                
                var order = _mapper.Map<Order>(request);
                await _unitOfWork.Orders.AddAsync(order);
                order.AccountId = claim.Id;
                var distanceResponse = await _googleMapService.GetDistanceAsync(request.FromAddress, request.ToAddress);
                if (distanceResponse == null)
                {
                    return new ApiResponse().SetBadRequest("Unable to calculate distance.");
                }

                if (transportService.TransportType == TransportType.Local)
                {
                    var distanceData = Newtonsoft.Json.Linq.JObject.Parse(distanceResponse);
                    var distanceText = distanceData["rows"][0]["elements"][0]["distance"]["text"].ToString();
                    var distanceValue = double.Parse(distanceData["rows"][0]["elements"][0]["distance"]["value"].ToString());
                    var distanceValueInKm = distanceValue / 1000;
                    order.Distance = distanceValueInKm;
                }

                await _unitOfWork.SaveChangeAsync();
                var orderAfterSave = await _unitOfWork.Orders.GetAsync(x => x.Id == order.Id);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                //double totalPrice = await CaculateTotalPrice(orderAfterSave.Id);
                //order.TotalPrice = totalPrice;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add success");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var orders = await _unitOfWork.Orders.GetAllAsync(null, x => x.Include(x => x.TransportService).Include(x => x.OrderFishs));
                var orderResponse = _mapper.Map<List<OrderResponse>>(orders);
                return new ApiResponse().SetOk(orderResponse);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> DeleteOrderByIdAsync(int id)
        {
            try
            {

                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == id);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                await _unitOfWork.Orders.RemoveByIdAsync(order.Id);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("Order deleted successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllUserOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            var claim = _claim.GetUserClaim();
            try
            {
                var orders = await _unitOfWork.Orders.GetAllAsync(x => x.AccountId == claim.Id,
                    x => x.Include(x => x.TransportService).Include(x => x.OrderFishs));
                var orderResponse = _mapper.Map<List<OrderResponse>>(orders);
                return new ApiResponse().SetOk(orderResponse);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateStatusOrderToDelivering(int OrderId)
        {
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.OrderStatus = OrderStatusEnum.Delivering;
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk(order);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateStatusOrderToCompleted(int OrderId)
        {
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.OrderStatus = OrderStatusEnum.Completed;
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk(order);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateStatusOrderToCanceled(int OrderId)
        {
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.OrderStatus = OrderStatusEnum.Canceled;
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk(order);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateStatusOrderToPendingPickUp(int OrderId)
        {
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.OrderStatus = OrderStatusEnum.PendingPickUp;
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk(order);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        

        public async Task<ApiResponse> CreateFeedBackAsync(FeedbackRequest request)
        {
            ApiResponse apiresponse = new ApiResponse();
            try
            {
                var feedback = _mapper.Map<Order>(request);
                var orderExist = await _unitOfWork.Orders.GetAsync(x => x.Id == feedback.Id);
                if (orderExist == null)
                {
                    return apiresponse.SetNotFound("Can not found order id ");
                }
                if (orderExist.OrderStatus != OrderStatusEnum.Completed)
                {
                    return apiresponse.SetNotFound("Feedback can only be created for completed orders ");
                }
                await _unitOfWork.Feedbacks.AddAsync(feedback);
                await _unitOfWork.SaveChangeAsync();
                return apiresponse.SetOk("Add success");
            }
            catch (Exception ex)
            {
                return apiresponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> CaculateTotalPrice(int OrderId)
        {
            decimal totalPrice = 0;
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId,
                                                              x => x.Include(x => x.TransportService)
                                                                    .Include(x => x.OrderFishs));
                if (order == null)
                {
                    apiResponse.SetNotFound("Order not found");
                }
                var transportService = order.TransportService;
                if (transportService == null)
                {
                    throw new Exception("TransportService is not linked to the order");
                }
                var totalWeight = order.OrderFishs.Sum(fish => fish.Weight);
                var numberOfFishes = order.OrderFishs.Count;
                //var totalDistance = 100;
                if (numberOfFishes == 0)
                {
                    totalPrice = 0;
                    return apiResponse.SetOk(totalPrice);
                }

                var weightPrice = totalWeight * transportService.PricePerKg;
                var transportServicePrice = transportService.TransportPrice;
                var amountPrice = numberOfFishes * transportService.PricePerAmount;
                totalPrice = (decimal)(weightPrice + transportServicePrice + amountPrice);
                order.TotalPrice = totalPrice;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(totalPrice);

            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }

        }

    }
}
