using Application.Interface;
using Application.Request.TransportService;
using Application.Response;
using Application.Response.TransportService;
using AutoMapper;
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
        public async Task<ApiResponse> AddTransportServiceAsync(TransportServiceRequest transportServiceRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var transportService = _mapper.Map<TransportService>(transportServiceRequest);
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
                var responseList =  _mapper.Map<List<TransportServiceResponse>>(transportServices);
                return new ApiResponse().SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
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
    }
}
