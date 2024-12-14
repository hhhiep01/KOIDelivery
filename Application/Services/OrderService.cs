using Application.Interface;
using Application.Request.Feedback;
using Application.Request.Order;
using Application.Response;
using Application.Response.Order;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entity;
using MaxMind.GeoIP2.Responses;
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
        private IGoogleMapService _googleMapService;

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
                if (request.PaymentMethod == PaymentMethodEnum.Cash)
                {
                    Payment payment = new Payment();
                    payment.StatusPayment = StatusPayment.Pending;
                }
                var order = _mapper.Map<Order>(request);
                double distanceInKm = 0;
                if (transportService.TransportType == TransportType.Local)
                {
                    var distanceResponse = await _googleMapService.GetDistanceAsync(request.FromAddress, request.ToAddress);
                    if (distanceResponse == null)
                    {
                        return new ApiResponse().SetBadRequest("Unable to calculate distance.");
                    }

                    var distanceData = Newtonsoft.Json.Linq.JObject.Parse(distanceResponse);
                    var distanceInMeters = double.Parse(distanceData["rows"][0]["elements"][0]["distance"]["value"].ToString());
                    distanceInKm = distanceInMeters / 1000;
                }
                await _unitOfWork.Orders.AddAsync(order);
                order.Distance = distanceInKm;
                order.AccountId = claim.Id;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(order.Id);
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
                var orders = await _unitOfWork.Orders.GetAllAsync(null, x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                var orderResponse = _mapper.Map<List<OrderResponse>>(orders);
                return new ApiResponse().SetOk(orderResponse);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetOrderByIdAsyncAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == id, x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }

                var orderResponse = _mapper.Map<OrderResponse>(order);
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
                    x => x.Include(x => x.TransportService)
                            .Include(x => x.OrderFishs));
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

        public async Task<ApiResponse> UpdateStatusOrderToCanceled(UpdateOrderToCancelRequest updateOrderToCancelRequest)
        {
            try
            {

                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == updateOrderToCancelRequest.OrderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.OrderStatus = OrderStatusEnum.Canceled;
                order.ReasonToCancel = updateOrderToCancelRequest.ReasonToCancel;
                order.ReasonToCancel = updateOrderToCancelRequest.ReasonToCancel;
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
        public async Task<ApiResponse> GetAllProccessingOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderStatus == OrderStatusEnum.Processing,
                                                                                 x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                var orderProccessingList = _mapper.Map<List<OrderResponse>>(order);
                return new ApiResponse().SetOk(orderProccessingList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllPendingPickUpOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderStatus == OrderStatusEnum.PendingPickUp,
                                                                                     x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                var orderPendingPickUpList = _mapper.Map<List<OrderResponse>>(order);
                return new ApiResponse().SetOk(orderPendingPickUpList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllDeliveringOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderStatus == OrderStatusEnum.Delivering,
                     x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                var orderDeliveringList = _mapper.Map<List<OrderResponse>>(order);
                return new ApiResponse().SetOk(orderDeliveringList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllCompletedOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderStatus == OrderStatusEnum.Completed,
                                                                                    x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                var orderCompletedList = _mapper.Map<List<OrderResponse>>(order);
                return new ApiResponse().SetOk(orderCompletedList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllCanceledOrderAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderStatus == OrderStatusEnum.Canceled,
                     x => x.Include(x => x.TransportService)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishQualifications)
                                                                                 .Include(x => x.OrderFishs)
                                                                                 .ThenInclude(of => of.FishHealths));
                var orderCanceledList = _mapper.Map<List<OrderResponse>>(order);
                return new ApiResponse().SetOk(orderCanceledList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateStatusPaymentToVnPayByOrderIdAsync(int orderId)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.PaymentMethod = PaymentMethodEnum.VnPay;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Update success");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateStatusPaymentToCashByOrderIdAsync(int orderId)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return new ApiResponse().SetNotFound("Order not found");
                }
                order.PaymentMethod = PaymentMethodEnum.Cash;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Update success");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
    }
}
