using Application.Interface;
using Application.Request.Fish;
using Application.Request.FishHealth;
using Application.Response;
using Application.Response.Fish;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FishHealthService : IFishHealthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FishHealthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateFishHealthAsync(FishHealthRequest request)
        {
            ApiResponse apiresponse = new ApiResponse();
            try
            {
                var fishHealth = _mapper.Map<FishHealth>(request);
                var fishfishHealthExist = await _unitOfWork.OrderFishes.GetAsync(x => x.Id == fishHealth.OrderFishId);
                if (fishfishHealthExist == null)
                {
                    return apiresponse.SetNotFound("Can not found fish order id ");
                }

                await _unitOfWork.FishHealths.AddAsync(fishHealth);
                await _unitOfWork.SaveChangeAsync();
                return apiresponse.SetOk("Add success");
            }
            catch (Exception ex)
            {
                return apiresponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteFishHealthAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var fishHealth = await _unitOfWork.FishHealths.GetAsync(x => x.Id == id);
                if (fishHealth == null)
                {
                    return apiResponse.SetNotFound("Can not found fish Id: " + id);
                }
                await _unitOfWork.FishHealths.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk(fishHealth);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllFishHealthAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var fishHealth = await _unitOfWork.FishHealths.GetAllAsync(null);
                var fishHealthResponse = _mapper.Map<List<FishHealth>>(fishHealth);

                return apiResponse.SetOk(fishHealthResponse);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetFishHealthByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var fishHealthService = await _unitOfWork.FishHealths.GetAsync(x => x.Id == id);
                if (fishHealthService is null)
                {
                    return apiResponse.SetBadRequest("Can not found fishHealthService Id : " + id);
                }
                var response = _mapper.Map<FishHealthResponse>(fishHealthService);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateFishHealthAsync(FishHealthUpdateRequest fishHealthUpdateRequest)
        {
            try
            {
                var fishHealthService = await _unitOfWork.FishHealths.GetAsync(x => x.Id == fishHealthUpdateRequest.Id);
                if (fishHealthService == null)
                {
                    return new ApiResponse().SetNotFound("Can not found fishHealthService Id : " + fishHealthUpdateRequest.Id);
                }
                fishHealthService.HealthStatus = fishHealthService.HealthStatus;
                fishHealthService.CheckDate = fishHealthService.CheckDate;
                fishHealthService.Notes = fishHealthService.Notes;
                fishHealthService.Temperature = fishHealthService.Temperature;
                fishHealthService.WaterQuality = fishHealthService.WaterQuality;
                fishHealthService.Behavior = fishHealthService.Behavior;

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("FishHealthService update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
