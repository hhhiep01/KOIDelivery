﻿using Application.Interface;
using Application.Request.Driver;
using Application.Request.RouteStop;
using Application.Response;
using Application.Response.Driver;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DriverService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> AddNewDriverAsync(DriverRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var account = await _unitOfWork.UserAccounts.GetAsync(x => x.Id == request.UserAccountId);
                if (account == null)
                {
                    return apiResponse.SetNotFound();
                }
                if (account.Role != Role.DeliveringStaff)
                {
                    return apiResponse.SetBadRequest("User is not a Delivering Staff");
                }

                var driver = _mapper.Map<Driver>(request);
                driver.Status = DriverStatus.Available;
                driver.UserAccount = account;

                await _unitOfWork.Drivers.AddAsync(driver);
                await _unitOfWork.SaveChangeAsync();

                return apiResponse.SetOk(driver);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllDriverAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var drivers = await _unitOfWork.Drivers.GetAllAsync(null);
                var driverList = _mapper.Map<List<DriverResponse>>(drivers);
                return apiResponse.SetOk(driverList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetDriverByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var driver = await _unitOfWork.Drivers.GetAsync(x => x.Id == id);
                if (driver == null)
                {
                    return apiResponse.SetBadRequest("Can not found driver id : " + id);
                }
                var response = _mapper.Map<DriverResponse>(driver);
                return apiResponse.SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateDriverByIdAsync(UpdateDriverRequest request)
        {
            try
            {
                var driver = await _unitOfWork.Drivers.GetAsync(x => x.Id == request.Id);
                if (driver == null)
                {
                    return new ApiResponse().SetNotFound("Can not found driver id : " + request.Id);
                }

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("Driver update successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteDriverByIdAsync(int id)
        {
            try
            {
                var driver = await _unitOfWork.Drivers.GetAsync(x => x.Id == id);
                if (driver == null)
                {
                    return new ApiResponse().SetNotFound("Driver not found");
                }
                await _unitOfWork.Drivers.RemoveByIdAsync(driver.Id);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("Driver deleted successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}