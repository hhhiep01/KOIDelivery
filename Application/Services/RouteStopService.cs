using Application.Interface;
using Application.Request.RouteStop;
using Application.Response;
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

        public async Task<ApiResponse> AddNewRouteStop(RouteStopRequest request)
        {
            try
            {
                await ResetRouteStopIdSequenceAsync();
                var routeStop = _mapper.Map<RouteStop>(request);
                await _unitOfWork.RouteStops.AddAsync(routeStop);
                await _unitOfWork.SaveChangeAsync();
                return new ApiResponse().SetOk(routeStop);
            }
            catch (Exception ex)
            {
                return new ApiResponse().SetBadRequest(ex.Message);
            }
        }

        //public async Task<ApiResponse> UpdateRouteStop(int routeId, int stopId, RouteStopRequest request)
        //{
        //    ApiResponse apiResponse = new ApiResponse();
        //    try
        //    {
        //        var route = await _unitOfWork.Routes.GetAsync(x => x.Id == request.Id);
        //    }
        //}
    }
}
