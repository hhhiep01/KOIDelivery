using Application.Interface;
using Application.Request.TransportService;
using Application.Response;
using Application.Response.TransportService;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransportServiceService : ITransportServiceService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public TransportServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> AddTransportLocalServiceAsync(TransportLocalServiceRequest transportServiceRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var transportService = _mapper.Map<TransportService>(transportServiceRequest);
                transportService.TransportType = TransportType.Local;
                await _unitOfWork.TransportServices.AddAsync(transportService);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add Success!");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> AddTransportDomesticServiceAsync(TransportServiceRequest transportServiceRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var transportService = _mapper.Map<TransportService>(transportServiceRequest);
                transportService.TransportType = TransportType.Domestic;
                await _unitOfWork.TransportServices.AddAsync(transportService);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add Success!");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> AddTransportInternaltionalServiceAsync(TransportServiceRequest transportServiceRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var transportService = _mapper.Map<TransportService>(transportServiceRequest);
                transportService.TransportType = TransportType.International;
                await _unitOfWork.TransportServices.AddAsync(transportService);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Add Success!");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetAllTransportServiceAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var transportServices = await _unitOfWork.TransportServices.GetAllAsync(null);
                var responseList = _mapper.Map<List<TransportServiceResponse>>(transportServices);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetTransportServiceByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var transportService = await _unitOfWork.TransportServices.GetAsync(x => x.Id == id);
                if (transportService is null)
                {
                    return apiResponse.SetBadRequest("Can not found transportService Id : " + id);
                }
                var response = _mapper.Map<TransportServiceResponse>(transportService);
                return new ApiResponse().SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateTransportServiceByIdAsync(TransportServiceUpdateRequest transportServiceUpdateRequest)
        {
            try
            {
                var transportService = await _unitOfWork.TransportServices.GetAsync(x => x.Id == transportServiceUpdateRequest.Id);
                if (transportService == null)
                {
                    return new ApiResponse().SetNotFound("Can not found transportService Id : " + transportServiceUpdateRequest.Id);
                }
                transportService.Name = transportServiceUpdateRequest.Name;
                transportService.Description = transportServiceUpdateRequest.Description;   
                transportService.TransportType = transportServiceUpdateRequest.TransportType;
                transportService.TransportPrice = transportServiceUpdateRequest.TransportPrice;
                transportService.PricePerKg = transportServiceUpdateRequest.PricePerKg;
                transportService.PricePerAmount = transportServiceUpdateRequest.PricePerAmount;
                transportService.FromProvince = transportServiceUpdateRequest.FromProvince;
                transportService.ToProvince = transportServiceUpdateRequest.ToProvince;
                transportService.IsActive = transportServiceUpdateRequest.IsActive;
                
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("TransportService update successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> DeleteTransportServiceByIdAsync(int id)
        {
            try
            {

                var transportService = await _unitOfWork.TransportServices.GetAsync(x => x.Id == id);
                if (transportService == null)
                {
                    return new ApiResponse().SetNotFound("TransportService not found");
                }
                await _unitOfWork.TransportServices.RemoveByIdAsync(transportService.Id);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("TransportService deleted successfully!");

            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetTransportServiceLocal()
        {
            try
            {
                var transportServices = await _unitOfWork.TransportServices.GetAllAsync(x => x.TransportType == TransportType.Local);
                var responseList = _mapper.Map<List<TransportLocalServiceResponse>>(transportServices);
                return new ApiResponse().SetOk(responseList);                
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetTransportServiceDomestic()
        {
            try
            {
                var transportServices = await _unitOfWork.TransportServices.GetAllAsync(x => x.TransportType == TransportType.Domestic);
                var responseList = _mapper.Map<List<TransportServiceResponse>>(transportServices);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> GetTransportServiceInternational()
        {
            try
            {
                var transportServices = await _unitOfWork.TransportServices.GetAllAsync(x => x.TransportType == TransportType.International);
                var responseList = _mapper.Map<List<TransportServiceResponse>>(transportServices);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
