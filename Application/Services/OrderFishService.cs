using Application.Interface;
using Application.Request.Fish;
using Application.Request.Order;
using Application.Request.TransportService;
using Application.Response;
using Application.Response.Fish;
using Application.Response.TransportService;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderFishService : IOrderFishService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IFirebaseStorageService _firebaseStorageService;
        //private readonly ICaculateTotalPriceService _caculateTotalPriceService;
        public OrderFishService(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
            
        }

        public async Task<ApiResponse> CreateOrderFishAsync(OrderFishRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var orderFish = _mapper.Map<OrderFish>(request);
                var orderFishExist = await _unitOfWork.Orders.GetAsync(x => x.Id == orderFish.OrderId, x => x.Include(x => x.TransportService));
                if (orderFishExist == null)
                {
                    return apiResponse.SetNotFound("Can not found Order Id: ");
                }
                orderFish.FishImgURL = await _firebaseStorageService.UploadOrderFishUrl(request.OrderId.ToString(), request.File);

                await _unitOfWork.OrderFishes.AddAsync(orderFish);
                await _unitOfWork.SaveChangeAsync();

                /*if (orderFishExist.TransportService.TransportType == TransportType.Local )
                {
                    var calculateResponse = await _caculateTotalPriceService.CaculateTotalPriceLocal(orderFish.OrderId.Value);

                    if (!calculateResponse.IsSuccess)
                    {
                        return apiResponse.SetBadRequest("Failed to calculate total price: " + calculateResponse);
                    }
                }
                else if (orderFishExist.TransportService.TransportType == TransportType.International)
                {
                    var calculateResponse = await _caculateTotalPriceService.CaculateTotalPriceInternational(orderFish.OrderId.Value);

                    if (!calculateResponse.IsSuccess)
                    {
                        return apiResponse.SetBadRequest("Failed to calculate total price: " + calculateResponse);
                    }
                }
                else if( orderFishExist.TransportService.TransportType == TransportType.Domestic)
                {
                    var calculateResponse = await _caculateTotalPriceService.CaculateTotalPriceDomestic(orderFish.OrderId.Value);

                    if (!calculateResponse.IsSuccess)
                    {
                        return apiResponse.SetBadRequest("Failed to calculate total price: " + calculateResponse);
                    }
                }*/
                return apiResponse.SetOk(orderFish.Id);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }

        }

        public async Task<ApiResponse> DeleteOrderFishAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var orderFish = await _unitOfWork.OrderFishes.GetAsync(x => x.Id == id);
                if (orderFish == null)
                {
                    return apiResponse.SetNotFound("Can not found fish Id: " + id);
                }
                await _unitOfWork.OrderFishes.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk(orderFish);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllOrderFishAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                //var orderFish = await _unitOfWork.OrderFishes.GetAllAsync(null, x => x.Include(c => c.FishHealths).Include(a => a.FishQualifications));
                var orderFish = await _unitOfWork.OrderFishes.GetAllAsync(null, x => x.Include(c => c.FishHealths).Include(a => a.FishQualifications));
                var orderFishList = _mapper.Map<List<OrderFishResponse>>(orderFish);

                return apiResponse.SetOk(orderFishList);
            }
            catch (JsonException jsonEx)
            {
                return new ApiResponse().SetBadRequest($"JSON Error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khác
                return new ApiResponse().SetBadRequest($"Error: {ex.Message} - InnerException: {ex.InnerException?.Message}");
            }
        }

        public async Task<ApiResponse> GetOrderFishByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var orderFishService = await _unitOfWork.OrderFishes.GetAsync(x => x.Id == id);
                if (orderFishService is null)
                {
                    return apiResponse.SetBadRequest("Can not found orderFishService Id : " + id);
                }
                var response = _mapper.Map<OrderFishResponse>(orderFishService);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateOrderFishAsync(OrderFishUpdateRequest orderFishUpdateRequest)
        {
            try
            {
                var orderFishService = await _unitOfWork.OrderFishes.GetAsync(x => x.Id == orderFishUpdateRequest.Id);
                if (orderFishService == null)
                {
                    return new ApiResponse().SetNotFound("Can not found transportService Id : " + orderFishUpdateRequest.Id);
                }
                orderFishService.Name = orderFishUpdateRequest.Name;
                orderFishService.Age = orderFishUpdateRequest.Age;
                orderFishService.Weight = orderFishUpdateRequest.Weight;
                orderFishService.Length = orderFishUpdateRequest.Length;
                orderFishService.FishImgURL = orderFishUpdateRequest.FishImgURL;

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("OrderFishService update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllOrderFishByOrderIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                //var orderFish = await _unitOfWork.OrderFishes.GetAllAsync(null, x => x.Include(c => c.FishHealths).Include(a => a.FishQualifications));
                var orderFish = await _unitOfWork.OrderFishes.GetAllAsync(x => x.OrderId == id, x => x.Include(c => c.FishHealths).Include(a => a.FishQualifications));
                var orderFishList = _mapper.Map<List<OrderFishResponse>>(orderFish);

                return apiResponse.SetOk(orderFishList);
            }
            catch (JsonException jsonEx)
            {
                return new ApiResponse().SetBadRequest($"JSON Error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khác
                return new ApiResponse().SetBadRequest($"Error: {ex.Message} - InnerException: {ex.InnerException?.Message}");
            }
        }
        
    }

}
