using Application.Interface;
using Application.Request.Payment;
using Application.Response;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CashPaymentService : ICashPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IFirebaseStorageService _firebaseStorageService;
        public CashPaymentService(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
        }
        public async Task<ApiResponse> CreatePayment(PaymentRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == request.OrderId,
                                                      x => x.Include(x => x.OrderFishs)
                                                            .Include(x => x.TransportService));
                if (order == null)
                {
                    return apiResponse.SetNotFound("Order not found");
                }
                if (order.PaymentMethod != PaymentMethodEnum.Cash)
                {
                    return apiResponse.SetBadRequest("Order must have Payment Method Cash");
                }
                var existingPayment = await _unitOfWork.Payments.GetAsync(x => x.OrderId == request.OrderId);
                if (existingPayment != null)
                {
                    if (existingPayment.StatusPayment == StatusPayment.Pending || existingPayment.StatusPayment == StatusPayment.Paid)
                    {
                        return apiResponse.SetBadRequest("This order has already been paid or the payment is being processed.");
                    }
                }
                Payment payment = new Payment()
                {
                    StatusPayment = StatusPayment.Pending,
                    Amount = order.TotalPrice.Value,
                    OrderId = order.Id
                };
                await _unitOfWork.Payments.AddAsync(payment);
                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Payment Success");

            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
        public async Task<ApiResponse> UpdateStatusCashPaymentOrderToSuccess(PaymentRequest request)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var order = await _unitOfWork.Orders.GetAsync(x => x.Id == request.OrderId,
                                                      x => x.Include(x => x.OrderFishs)
                                                            .Include(x => x.TransportService));
                if (order == null)
                {
                    return apiResponse.SetNotFound("Order not found");
                }
                if (order.PaymentMethod != PaymentMethodEnum.Cash)
                {
                    return apiResponse.SetBadRequest("Order must have Payment Method Cash");
                }
                var existingPayment = await _unitOfWork.Payments.GetAsync(x => x.OrderId == request.OrderId);
                if (existingPayment != null)
                {
                    if (existingPayment.StatusPayment == StatusPayment.Paid)
                    {
                        return apiResponse.SetBadRequest("This order has already been paid");
                    }
                }
                existingPayment.StatusPayment = StatusPayment.Paid;

                await _unitOfWork.SaveChangeAsync();
                return apiResponse.SetOk("Payment Update To Paid");

            }
            catch (Exception ex)
            {
                return apiResponse.SetBadRequest(ex.Message);
            }
        }
    }
}

