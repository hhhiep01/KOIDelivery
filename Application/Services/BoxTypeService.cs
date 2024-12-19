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
using FluentValidation;

namespace Application.Services
{
    public class BoxTypeService: IBoxTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<BoxTypeRequest> _boxTypeValidator;
        public BoxTypeService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<BoxTypeRequest> boxTypeValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _boxTypeValidator = boxTypeValidator;
        }
        public async Task<ApiResponse> AddBoxTypeRequestAsync(BoxTypeRequest boxTypeRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var validationResult = await _boxTypeValidator.ValidateAsync(boxTypeRequest);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return apiResponse.SetBadRequest(string.Join(", ", errors));
                }

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

        public async Task<ApiResponse> DeleteBoxTypeAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var boxType = await _unitOfWork.BoxTypes.GetAsync(x => x.Id == id);
                if (boxType == null)
                {
                    return apiResponse.SetNotFound("Can not found box type Id: " + id);
                }
                await _unitOfWork.BoxTypes.RemoveByIdAsync(id);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk(boxType);
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

        public async Task<ApiResponse> GetBoxTypeByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var boxTypeService = await _unitOfWork.BoxTypes.GetAsync(x => x.Id == id);
                if (boxTypeService is null)
                {
                    return apiResponse.SetBadRequest("Can not found boxTypeService Id : " + id);
                }
                var response = _mapper.Map<BoxTypeResponse>(boxTypeService);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateBoxTypeAsync(BoxTypeUpdateRequest boxTypeUpdateRequest)
        {
            try
            {
                var boxType= await _unitOfWork.BoxTypes.GetAsync(x => x.Id == boxTypeUpdateRequest.Id);
                if (boxType == null)
                {
                    return new ApiResponse().SetNotFound("Can not found box type Id : " + boxTypeUpdateRequest.Id);
                }


                _mapper.Map(boxTypeUpdateRequest, boxType);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("box type update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
