using Application.Helper;
using Application.Interface;
using Application.Response;
using AutoMapper;
using MaxMind.GeoIP2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Request.Location;
using Domain.Entity;
namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void ProcessLocation(LocationRequest request)
        {
            Console.WriteLine($"[Service] Processing location: Latitude = {request.Latitude}, Longitude = {request.Longitude}");
        }

        public async Task<ApiResponse> UpdateLocationAsync(int id, LocationRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var driverToUpdate = await _unitOfWork.Drivers.GetAsync(d => d.Id == id);
                if (driverToUpdate == null) 
                {
                    return apiResponse.SetNotFound("Driver not found");
                }
                driverToUpdate.ModifiedDate = DateTime.Now;
                driverToUpdate.Latitude = request.Latitude;
                driverToUpdate.Longitude = request.Longitude;

                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Location updated successfully");
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

    }
}
