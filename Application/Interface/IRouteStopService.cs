using Application.Request.RouteStop;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRouteStopService
    {
        Task<ApiResponse> AddNewRouteStopAsync(RouteStopRequest request);

        Task<ApiResponse> GetAllRouteStopAsync();

        Task<ApiResponse> GetAllRouteStopByRouteIdAsync(int id);

        Task<ApiResponse> GetRouteStopByIdAsync(int id);

        Task<ApiResponse> UpdateRouteStopByIdAsync(UpdateRouteStopRequest request);

        Task<ApiResponse> DeleteRouteStopByIdAsync(int id);
    }
}
