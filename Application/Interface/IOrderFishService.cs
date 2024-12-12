using Application.Request.Fish;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderFishService
    {
        Task<ApiResponse> CreateOrderFishAsync(OrderFishRequest request);
        Task<ApiResponse> GetAllOrderFishAsync();
        Task<ApiResponse> DeleteOrderFishAsync(int id);
        Task<ApiResponse> GetOrderFishByIdAsync(int id);
        Task<ApiResponse> UpdateOrderFishAsync(OrderFishUpdateRequest orderFishUpdateRequest);
        Task<ApiResponse> GetAllOrderFishByOrderIdAsync(int id);
    }
}
