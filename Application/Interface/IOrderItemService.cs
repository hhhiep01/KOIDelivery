using Application.Request.OrderItem;
using Application.Response;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderItemService
    {
        Task<ApiResponse> AddOrderItemAsync(OrderItemRequest orderItemRequest);
        Task<ApiResponse> GetAllOrderItemAsync();


    }
}
