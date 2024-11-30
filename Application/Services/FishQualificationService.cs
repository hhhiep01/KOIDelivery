using Application.Interface;
using Application.Request.Fish;
using Application.Response;
using Application.Response.Fish;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FishQualificationService : IFishQualificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FishQualificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }
        public async Task<ApiResponse> CreateFishQualificationAsync(FishQualificationRequest request)
        {
            ApiResponse apiresponse = new ApiResponse();
            try
            {
                var fishqualification = _mapper.Map<FishQualification>(request);
                var fishQualificationExist = await _unitOfWork.OrderFishes.GetAsync(x => x.Id == fishqualification.OrderFishId);
                if (fishQualificationExist == null)
                {
                    return apiresponse.SetNotFound("Can not found fish order id " );
                }

                fishqualification.CreateAt = DateTime.Now;
                fishqualification.UpdatedAt = DateTime.Now;

                await _unitOfWork.FishQualifications.AddAsync(fishqualification);
                await _unitOfWork.SaveChangeAsync();
                return apiresponse.SetOk("Add success");
            }catch (Exception ex) 
            {
                return apiresponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteFishQualificationAsync(int id)
        {
                ApiResponse apiResponse = new ApiResponse();
                try
                {
                    var fishQualification = await _unitOfWork.FishQualifications.GetAsync(x => x.Id == id);
                    if (fishQualification == null)
                    {
                        return apiResponse.SetNotFound("Can not found fish Id: " + id);
                    }
                    await _unitOfWork.FishQualifications.RemoveByIdAsync(id);
                    await _unitOfWork.SaveChangeAsync();

                    return apiResponse.SetOk(fishQualification);
                }
                catch (Exception ex)
                {
                    return apiResponse.SetBadRequest(ex.Message);
                }
            }

        public async Task<ApiResponse> GetAllFishQualificationAsync()
        {
                ApiResponse apiResponse = new ApiResponse();
                try
                {
                    var fishQualification = await _unitOfWork.FishQualifications.GetAllAsync(null);
                    var fishQualificationList= _mapper.Map<List<FishQualificationResponse>>(fishQualification);

                    return apiResponse.SetOk(fishQualificationList);
                }
                catch (Exception ex)
                {
                    return apiResponse.SetBadRequest(ex.Message);
                }
            }

        public async Task<ApiResponse> GetFishQualificationByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var fishQualificationService = await _unitOfWork.FishQualifications.GetAsync(x => x.Id == id);
                if (fishQualificationService is null)
                {
                    return apiResponse.SetBadRequest("Can not found fishQualificationService Id : " + id);
                }
                var response = _mapper.Map<FishHealthResponse>(fishQualificationService);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateFishQualificationAsync(FishQualificationUpdateRequest fishQualificationUpdateRequest)
        {
            try
            {
                var fishQualificationService = await _unitOfWork.FishQualifications.GetAsync(x => x.Id == fishQualificationUpdateRequest.Id);
                if (fishQualificationService == null)
                {
                    return new ApiResponse().SetNotFound("Can not found fishQualificationService Id : " + fishQualificationUpdateRequest.Id);
                }
                fishQualificationService.CreateAt = fishQualificationService.CreateAt;
                fishQualificationService.UpdatedAt = fishQualificationService.UpdatedAt;
                fishQualificationService.ImageUrl = fishQualificationService.ImageUrl;

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("FishQualificationService update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
