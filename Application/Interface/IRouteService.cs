using Application.Request.Route;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRouteService
    {
        Task<ApiResponse> AddRouteAsync(RouteRequest request);

        Task<ApiResponse> GetAllRouteAsync();

        Task<ApiResponse> GetRouteByIdAsync(int id);

        Task<ApiResponse> DeleteRouteByIdAsync(int id);
    }
}
