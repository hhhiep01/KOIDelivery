using Application.Interface;
using Application.Request.RouteStop;
using Application.Response;
using Application.Response.RouteStop;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RouteStopService : IRouteStopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RouteStopService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ResetRouteStopIdSequenceAsync()
        {
            var x = await _unitOfWork.RouteStops.CountAsync();
            if (x <= 0)
            {
                return;
            }


            ApiResponse apiResponse = new ApiResponse();

            // Lấy giá trị MAX(Id) từ bảng RouteStops
            string maxIdSql = "SELECT COALESCE(MAX(Id), 0) FROM RouteStops";
            int maxId = await _unitOfWork.ExecuteScalarAsync<int>(maxIdSql);

            // Đặt lại IDENTITY cho cột Id
            string resetIdentitySql = $"DBCC CHECKIDENT ('RouteStops', RESEED, {maxId})";
            await _unitOfWork.ExecuteRawSqlAsync(resetIdentitySql);
        }

        public async Task<ApiResponse> AddNewRouteStopAsync(RouteStopRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routeStop = _mapper.Map<RouteStop>(request);
                await _unitOfWork.RouteStops.AddAsync(routeStop);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(routeStop);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllRouteStopAsync()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routeStop = await _unitOfWork.RouteStops.GetAllAsync(null);
                var responseList = _mapper.Map<List<RouteStopResponse>>(routeStop);
                return apiResponse.SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllRouteStopByRouteIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routeStops = await _unitOfWork.RouteStops.GetAllAsync(x => x.RouteId == id);

                var responseList = _mapper.Map<List<RouteStopResponse>>(routeStops);

                return apiResponse.SetOk(responseList);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> GetRouteStopByIdAsync(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var routeStop = await _unitOfWork.RouteStops.GetAsync(x => x.Id == id);
                if (routeStop == null)
                {
                    return apiResponse.SetBadRequest("Can not found route stop id : " + id);
                }
                var response = _mapper.Map<RouteStopResponse>(routeStop);
                return apiResponse.SetOk(response);
            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateRouteStopByIdAsync(UpdateRouteStopRequest request)
        {
            try
            {
                var routeStop = await _unitOfWork.RouteStops.GetAsync(x => x.Id == request.Id);
                if (routeStop == null)
                {
                    return new ApiResponse().SetNotFound("Can not found route stop id : " + request.Id);
                }

                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("Route stop update successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteRouteStopByIdAsync(int id)
        {
            try
            {
                var routeStop = await _unitOfWork.RouteStops.GetAsync(x => x.Id == id);
                if (routeStop == null)
                {
                    return new ApiResponse().SetNotFound("Route stop not found");
                }
                await _unitOfWork.RouteStops.RemoveByIdAsync(routeStop.Id);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk("Route stop deleted successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }
    }
}
