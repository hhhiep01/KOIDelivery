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
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IClaimService _claim;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claim)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claim = claim;
        }

        public async Task<ApiResponse> CreateOrderAsync(OrderRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                
                var order = _mapper.Map<Order>(request);
                var userId = _claim.GetUserClaim();
               
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add success");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
    }
}
