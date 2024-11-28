using Application.Request.Fish;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFishQualificationService
    {
        Task<ApiResponse> CreateFishQualificationAsync(FishQualificationRequest request);
        Task<ApiResponse> GetAllFishQualificationAsync();
        Task<ApiResponse> DeleteFishQualificationAsync(int id);
        Task<ApiResponse> GetFishQualificationByIdAsync(int id);
        Task<ApiResponse> UpdateFishQualificationAsync(FishQualificationUpdateRequest fishQualificationUpdateRequest);
    }
}
