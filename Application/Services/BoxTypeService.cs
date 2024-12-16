using Application.Interface;
using Application.Request.KoiSize;
using Application.Response.KoiSize;
using Application.Response;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Request.BoxType;
using Application.Response.BoxType;

namespace Application.Services
{
    public class BoxTypeService: IBoxTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public BoxTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> AddBoxTypeRequestAsync(BoxTypeRequest boxTypeRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var boxType = _mapper.Map<BoxType>(boxTypeRequest);
                await _unitOfWork.BoxTypes.AddAsync(boxType);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add Success!");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAlBoxTypeAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var boxTypes = await _unitOfWork.BoxTypes.GetAllAsync(null);
                var responseList = _mapper.Map<List<BoxTypeResponse>>(boxTypes);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
    }
}
