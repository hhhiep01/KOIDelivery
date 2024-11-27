using Application.Interface;
using Application.Request.Order;
using Application.Response;
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
        public async Task<ApiResponse> AddOrderFish(OrderFishService request)
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
                    return apiResponse.SetNotFound("Con not found fish Id: " + id);
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
                var orderFishResponse = _mapper.Map<List<OrderFish>>(orderFish);

                return apiResponse.SetOk(orderFishResponse);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

    }
}
