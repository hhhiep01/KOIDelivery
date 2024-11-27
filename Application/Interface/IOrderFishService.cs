using Application.Request.Order;
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
    }
}
