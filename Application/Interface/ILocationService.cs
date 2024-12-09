using Application.Request.Location;
using Application.Response;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ILocationService
    {
        void ProcessLocation(LocationRequest request);
        Task<ApiResponse> UpdateLocationAsync(int id, LocationRequest request);
    }
}
