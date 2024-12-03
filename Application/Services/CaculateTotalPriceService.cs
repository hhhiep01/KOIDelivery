using Application.Interface;
using Application.Response;
using AutoMapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CaculateTotalPriceService : ICaculateTotalPriceService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IFirebaseStorageService _firebaseStorageService;
        public CaculateTotalPriceService(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
        }
        public async Task<ApiResponse> CaculateTotalPriceLocal(int OrderId)
        {
            decimal totalPrice = 0;
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId,
                                                              x => x.Include(x => x.TransportService)
                                                                    .Include(x => x.OrderFishs));
                if (order == null)
                {
                    apiResponse.SetNotFound("Order not found");
                }
                var transportService = order.TransportService;
                if (transportService == null)
                {
                    throw new Exception("TransportService is not linked to the order");
                }
                if (transportService.TransportType != TransportType.Local)
                {
                    throw new Exception("TransportService must be Local");
                }
                var totalWeight = order.OrderFishs.Sum(fish => fish.Weight);
                var numberOfFishes = order.OrderFishs.Count;
                //var totalDistance = 100;
                if (numberOfFishes == 0)
                {
                    totalPrice = 0;
                    return apiResponse.SetOk(totalPrice);
                }
                var totalKm = order.Distance;
                var weightPrice = totalWeight * transportService.PricePerKg;
                //var transportServicePrice = transportService.TransportPrice;
                var kmPrice = totalKm * transportService.PricePerKm;
                var amountPrice = numberOfFishes * transportService.PricePerAmount;
                totalPrice = (decimal)(weightPrice + kmPrice + amountPrice);
                order.TotalPrice = totalPrice;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(totalPrice);

            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }

        }
        public async Task<ApiResponse> CaculateTotalPriceDomestic(int OrderId)
        {
            decimal totalPrice = 0;
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId,
                                                              x => x.Include(x => x.TransportService)
                                                                    .Include(x => x.OrderFishs));
                if (order == null)
                {
                    apiResponse.SetNotFound("Order not found");
                }
                var transportService = order.TransportService;
                if (transportService == null)
                {
                    throw new Exception("TransportService is not linked to the order");
                }
                if (transportService.TransportType != TransportType.Domestic)
                {
                    throw new Exception("TransportService must be Domestic");
                }
                var totalWeight = order.OrderFishs.Sum(fish => fish.Weight);
                var numberOfFishes = order.OrderFishs.Count;
                //var totalDistance = 100;
                if (numberOfFishes == 0)
                {
                    totalPrice = 0;
                    return apiResponse.SetOk(totalPrice);
                }
                var totalKm = order.Distance;
                var weightPrice = totalWeight * transportService.PricePerKg;
                var transportServicePrice = transportService.TransportPrice;
                //var kmPrice = totalKm * transportService.PricePerKm;
                var amountPrice = numberOfFishes * transportService.PricePerAmount;
                totalPrice = (decimal)(weightPrice + transportServicePrice + amountPrice);
                order.TotalPrice = totalPrice;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(totalPrice);

            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }

        }
        public async Task<ApiResponse> CaculateTotalPriceInternational(int OrderId)
        {
            decimal totalPrice = 0;
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == OrderId,
                                                              x => x.Include(x => x.TransportService)
                                                                    .Include(x => x.OrderFishs));
                if (order == null)
                {
                    apiResponse.SetNotFound("Order not found");
                }
                var transportService = order.TransportService;
                if (transportService == null)
                {
                    throw new Exception("TransportService is not linked to the order");
                }
                if (transportService.TransportType != TransportType.International)
                {
                    throw new Exception("TransportService must be International");
                }
                var totalWeight = order.OrderFishs.Sum(fish => fish.Weight);
                var numberOfFishes = order.OrderFishs.Count;
                //var totalDistance = 100;
                if (numberOfFishes == 0)
                {
                    totalPrice = 0;
                    return apiResponse.SetOk(totalPrice);
                }
                var totalKm = order.Distance;
                var weightPrice = totalWeight * transportService.PricePerKg;
                var transportServicePrice = transportService.TransportPrice;
                //var kmPrice = totalKm * transportService.PricePerKm;
                var amountPrice = numberOfFishes * transportService.PricePerAmount;
                totalPrice = (decimal)(weightPrice + transportServicePrice + amountPrice);
                order.TotalPrice = totalPrice;
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk(totalPrice);

            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }

        }
    }
}
