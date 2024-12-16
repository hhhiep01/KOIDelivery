using Application.Request.Fish;
using Application.Request.FishDetail;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFishDetailService
    {
        Task<ApiResponse> CreateFishDetailAsync(FishDetailRequest request);
        Task<ApiResponse> GetAllFishDetailAsync();
        Task<ApiResponse> DeleteFishDetailAsync(int id);
        Task<ApiResponse> GetFishDetailByIdAsync(int id);
        Task<ApiResponse> UpdateFishDetailAsync(FishDetailUpdateRequest fishDetailUpdateRequest);
        Task<ApiResponse> GetAllFishDetailByOrderIdAsync(int id);
    }
}
