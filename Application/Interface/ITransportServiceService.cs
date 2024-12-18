﻿using Application.Request.TransportService;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ITransportServiceService
    {
        Task<ApiResponse> AddTransportLocalServiceAsync(TransportLocalServiceRequest transportServiceRequest);
        Task<ApiResponse> AddTransportDomesticServiceAsync(TransportServiceRequest transportServiceRequest);
        Task<ApiResponse> AddTransportInternaltionalServiceAsync(TransportServiceRequest transportServiceRequest);
        Task<ApiResponse> GetAllTransportServiceAsync();
        Task<ApiResponse> DeleteTransportServiceByIdAsync(int id);
        Task<ApiResponse> GetTransportServiceByIdAsync(int id);
        Task<ApiResponse> UpdateTransportServiceByIdAsync(TransportServiceUpdateRequest transportServiceUpdateRequest);
        Task<ApiResponse> GetTransportServiceInternational();
        Task<ApiResponse> GetTransportServiceDomestic();
        Task<ApiResponse> GetTransportServiceLocal();
    }
}
