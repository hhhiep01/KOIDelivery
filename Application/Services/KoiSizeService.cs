using Application.Interface;
using Application.Request.KoiSize;
using Application.Request.TransportService;
using Application.Response;
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
    }
}
