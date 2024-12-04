using Application.Request.Fish;
using Application.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFishQualificationService
    {
        Task<ApiResponse> CreateFishQualificationAsync(FishQualificationRequest request, IFormFile file);
        Task<ApiResponse> GetAllFishQualificationAsync();
        Task<ApiResponse> DeleteFishQualificationAsync(int id);
        Task<ApiResponse> GetFishQualificationByIdAsync(int id);
        Task<ApiResponse> UpdateFishQualificationAsync(FishQualificationUpdateRequest fishQualificationUpdateRequest);
    }
}
