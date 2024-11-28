using Application.Interface;
using Application.Request.Fish;
using Application.Request.TransportService;
using Application.Response;
using Application.Response.Fish;
using Application.Response.TransportService;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderFishService : IOrderFishService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public OrderFishService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateOrderFishAsync(OrderFishRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var orderFish = _mapper.Map<OrderFish>(request);

                await _unitOfWork.OrderFishes.AddAsync(orderFish);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add success");
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
                var orderFish = await _unitOfWork.OrderFishes.GetAllAsync(null);
                var orderFishList = _mapper.Map<List<OrderFishResponse>>(orderFish);

                return apiResponse.SetOk(orderFishList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
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
    }
}
