using Application.Interface;
using Application.Request.Fish;
using Application.Request.KoiSize;
using Application.Request.TransportService;
using Application.Response;
using Application.Response.Fish;
using Application.Response.KoiSize;
using Application.Response.TransportService;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KoiSizeService : IKoiSizeService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public KoiSizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> AddKoiSizeAsync(KoiSizeRequest koiSizeRequest)
        {   
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var koiSize = _mapper.Map<KoiSize>(koiSizeRequest);
                await _unitOfWork.KoiSizes.AddAsync(koiSize);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add Success!");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteKoiSizeAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var koiSize = await _unitOfWork.KoiSizes.GetAsync(x => x.Id == id);
                if (koiSize == null)
                {
                    return apiResponse.SetNotFound("Can not found koi size Id: " + id);
                }
                await _unitOfWork.KoiSizes.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk(koiSize);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllKoiSizeAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var koiSizes = await _unitOfWork.KoiSizes.GetAllAsync(null);
                var responseList = _mapper.Map<List<KoiSizeResponse>>(koiSizes);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetKoiSizeByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var koiSizeService = await _unitOfWork.KoiSizes.GetAsync(x => x.Id == id);
                if (koiSizeService is null)
                {
                    return apiResponse.SetBadRequest("Can not found koi size Id : " + id);
                }
                var response = _mapper.Map<KoiSizeResponse>(koiSizeService);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateKoiSizeAsync(KoiSizeUpdateRequest koiSizeUpdateRequest)
        {
            try
            {
                var koiSizeService = await _unitOfWork.KoiSizes.GetAsync(x => x.Id == koiSizeUpdateRequest.Id);
                if (koiSizeService == null)
                {
                    return new ApiResponse().SetNotFound("Can not found koi size Id : " + koiSizeUpdateRequest.Id);
                }
                koiSizeService.SizeCmMin = koiSizeUpdateRequest.SizeCmMin;
                koiSizeService.SizeCmMax = koiSizeUpdateRequest.SizeCmMax;
                koiSizeService.SizeInchMin = koiSizeUpdateRequest.SizeInchMin;
                koiSizeService.SizeInchMax = koiSizeUpdateRequest.SizeInchMax;
                koiSizeService.SpaceRequired = koiSizeUpdateRequest.SpaceRequired;

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("koi size update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
