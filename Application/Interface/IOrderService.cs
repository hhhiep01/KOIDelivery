using Application.Request.Feedback;
using Application.Request.Order;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderService
    {
        Task<ApiResponse> CreateOrderAsync(OrderRequest request);
        Task<ApiResponse> GetAllOrderAsync();
        Task<ApiResponse> DeleteOrderByIdAsync(int id);
        Task<ApiResponse> GetAllUserOrderAsync();
        Task<ApiResponse> UpdateStatusOrderToDelivering(int OrderId);
        Task<ApiResponse> UpdateStatusOrderToCompleted(int OrderId);
        Task<ApiResponse> UpdateStatusOrderToCanceled(int OrderId);
        Task<ApiResponse> UpdateStatusOrderToPendingPickUp(int OrderId);
        Task<ApiResponse> CreateFeedBackAsync(FeedbackRequest request);
        Task<ApiResponse> GetAllProccessingOrderAsync();
    }
}
