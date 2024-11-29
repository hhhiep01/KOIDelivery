using Application.Request.Driver;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IDriverService
    {
        Task<ApiResponse> AddNewDriverAsync(DriverRequest request);
        Task<ApiResponse> GetAllDriverAsync();
        Task<ApiResponse> GetDriverByIdAsync(int id);
        Task<ApiResponse> UpdateDriverByIdAsync(UpdateDriverRequest request);
        Task<ApiResponse> DeleteDriverByIdAsync(int id);
    }
}
