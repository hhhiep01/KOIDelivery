using Application.Request.Fish;
using Application.Request.FishHealth;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFishHealthService
    {
        Task<ApiResponse> CreateFishHealthAsync(FishHealthRequest request);
        Task<ApiResponse> GetAllFishHealthAsync();
        Task<ApiResponse> DeleteFishHealthAsync(int id);
        Task<ApiResponse> GetFishHealthByIdAsync(int id);
        Task<ApiResponse> UpdateFishHealthAsync(FishHealthUpdateRequest fishHealthUpdateRequest);
    }
}
