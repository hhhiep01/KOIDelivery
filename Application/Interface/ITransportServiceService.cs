using Application.Request.TransportService;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ITransportServiceService
    {
        Task<ApiResponse> AddTransportServiceAsync(TransportServiceRequest transportServiceRequest);
        Task<ApiResponse> GetAllTransportServiceAsync();
        Task<ApiResponse> DeleteTransportServiceByIdAsync(int id);
    }
}
