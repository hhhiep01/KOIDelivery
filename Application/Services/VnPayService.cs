using Application.Interface;
using Application.Request.Payment;
using Application.Response;
using Application.Response.Payment;
using Azure;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VnPayService : IVnPayService
    {

        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        public VnPayService(IConfiguration configuration, IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }
        public async Task<ApiResponse> CreatePaymentUrl(PaymentRequest request, HttpContext context)
        {
            ApiResponse response = new ApiResponse();
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
           
            var model = new PaymentInformation();
            var claim = _claimService.GetUserClaim();
            var userId = claim.Id;
            var order = await _unitOfWork.Orders.GetAsync(x => x.Id == request.OrderId);
            if (order is null)
            {
                return response.SetNotFound("Order Id not found");
            }
            var payment = await _unitOfWork.Payments.GetAsync(x => x.OrderId == request.OrderId);
            if(payment!=null && payment.StatusPayment == StatusPayment.Paid)
            {
                return response.SetNotFound("This order is Paid");
            }
            //payment.Amount = order.TotalPrice;
            model.Amount = order.TotalPrice;
            model.Name = "Pro Supcription";
            model.OrderDescription = "Pro Supcription";
            model.OrderType = "Pro";

            
            var urlCallBack = $"{_configuration["PaymentCallBack:ReturnUrl"]}?userId={claim.Id}&amount={model.Amount}&orderId={order.Id}";


            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return response.SetOk(paymentUrl);
        }
        public async Task<ApiResponse> PaymentExecute(IQueryCollection collections)
        {
            try
            {
                ApiResponse apiResponse = new ApiResponse();
                var pay = new VnPayLibrary();
                var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

                // Extract userId from query parameters
                if (collections.TryGetValue("userId", out var userIdValue) &&
                    int.TryParse(userIdValue, out int userId) &&
                    collections.TryGetValue("orderId", out var orderIdValue) &&
                    int.TryParse(orderIdValue, out int orderId))
                {
                    // Check the response code to ensure the payment was successful
                    if (response.Success)
                    {
                        // Payment was successful
                        var user = await _unitOfWork.UserAccounts.GetAsync(x => x.Id == userId);
                        var order = await _unitOfWork.Orders.GetAsync(x => x.Id == orderId);
                        if (user != null && order is not null)
                        {
                            if (collections.TryGetValue("amount", out var amountValue) && int.TryParse(amountValue, out int amount))
                            {   

                                Payment payment = new Payment();                                
                                if (amount == order.TotalPrice)
                                {
                                    payment.Amount = amount;
                                    payment.StatusPayment = StatusPayment.Paid;
                                    payment.OrderId = orderId;

                                }
                                else
                                {
                                    payment.StatusPayment = StatusPayment.Failed;
                                    payment.OrderId = orderId;
                                    apiResponse.SetBadRequest("The payment amount does not match the order amount.");
                                    return apiResponse;
                                }
                                await _unitOfWork.Payments.AddAsync(payment);

                            }
                            else
                            {
                                return apiResponse.SetBadRequest("parse error");
                            }

                            await _unitOfWork.SaveChangeAsync();
                            var redirectUrl = "http://localhost:5173/it-jobs?status=success";
                            return apiResponse.SetOk(redirectUrl);
                        }
                        else
                        {
                            return apiResponse.SetBadRequest("User not found");
                        }
                    }
                    else
                    {
                        // Payment failed
                        return apiResponse.SetOk("VN Pay api respone fail");
                    }
                }
                else
                {
                    return apiResponse.SetBadRequest("Invalid or missing userId From call back url");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
