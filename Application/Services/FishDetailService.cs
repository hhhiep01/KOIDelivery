using Application.Interface;
using Application.Request.BoxType;
using Application.Request.Fish;
using Application.Request.FishDetail;
using Application.Response;
using Application.Response.Fish;
using Application.Response.FishDetail;
using Application.Validation;
using AutoMapper;
using Domain.Entity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FishDetailService : IFishDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly IValidator<FishDetailRequest> _fishDetailValidator;
        //private readonly ICaculateTotalPriceService _caculateTotalPriceService;
        public FishDetailService(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseStorageService firebaseStorageService, IValidator<FishDetailRequest> fishDetailValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
            _fishDetailValidator = fishDetailValidator;

        }
        public async Task<ApiResponse> CreateFishDetailAsync(FishDetailRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var validationResult = await _fishDetailValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return apiResponse.SetBadRequest(string.Join(", ", errors));
                }
                var fishDetail = _mapper.Map<FishDetail>(request);
                var fishDetailExist = await _unitOfWork.Orders.GetAsync(x => x.Id == fishDetail.Id, x => x.Include(x => x.TransportService));
                if (fishDetailExist == null)
                {
                    return apiResponse.SetNotFound("Can not found Order Id: ");
                }
                fishDetail.FishImgURL = await _firebaseStorageService.UploadFishDetailUrl(request.OrderItemId.ToString(), request.File);

                await _unitOfWork.FishDetails.AddAsync(fishDetail);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(fishDetail.Id);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteFishDetailAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var fishDetail = await _unitOfWork.FishDetails.GetAsync(x => x.Id == id);
                if (fishDetail == null)
                {
                    return apiResponse.SetNotFound("Can not found fish Id: " + id);
                }
                await _unitOfWork.FishDetails.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk(fishDetail);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllFishDetailAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                //var orderFish = await _unitOfWork.OrderFishes.GetAllAsync(null, x => x.Include(c => c.FishHealths).Include(a => a.FishQualifications));
                var fishDetail = await _unitOfWork.FishDetails.GetAllAsync(null, x => x.Include(c => c.FishHealths).Include(a => a.FishQualifications));
                var fishDetailList = _mapper.Map<List<FishDetailResponse>>(fishDetail);

                return apiResponse.SetOk(fishDetailList);
            }
            catch (JsonException jsonEx)
            {
                return new ApiResponse().SetBadRequest($"JSON Error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khác
                return new ApiResponse().SetBadRequest($"Error: {ex.Message} - InnerException: {ex.InnerException?.Message}");
            }
        }

            public async Task<ApiResponse> GetAllFishDetailByOrderIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetFishDetailByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var fishDetail = await _unitOfWork.FishDetails.GetAsync(x => x.Id == id);
                if (fishDetail is null)
                {
                    return apiResponse.SetBadRequest("Can not found fishDetail Id : " + id);
                }
                var response = _mapper.Map<FishDetailResponse>(fishDetail);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateFishDetailAsync(FishDetailUpdateRequest fishDetailUpdateRequest)
        {

            try
            {
                var fishDetail = await _unitOfWork.FishDetails.GetAsync(x => x.Id == fishDetailUpdateRequest.Id);
                if (fishDetail == null)
                {
                    return new ApiResponse().SetNotFound("Can not found fishDetail Id : " + fishDetailUpdateRequest.Id);
                }
                _mapper.Map(fishDetailUpdateRequest, fishDetail);

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("fishDetail update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
