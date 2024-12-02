using Application.Request.Payment;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICashPaymentService
    {
        Task<ApiResponse> CreatePayment(PaymentRequest request);
        Task<ApiResponse> UpdateStatusCashPaymentOrderToSuccess(PaymentRequest request);
    }
}
