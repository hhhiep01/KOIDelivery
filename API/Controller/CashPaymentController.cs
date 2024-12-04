using Application.Interface;
using Application.Request.Driver;
using Application.Request.Fish;
using Application.Request.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashPaymentController : ControllerBase
    {
        public ICashPaymentService _service;
        public CashPaymentController(ICashPaymentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentRequest paymentRequest)
        {
            var result = await _service.CreatePayment(paymentRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("UpdateStatusCashPaymentOrderToPaid")]
        public async Task<IActionResult> UpdateDriverByIdAsync(PaymentRequest paymentRequest)
        {
            var response = await _service.UpdateStatusCashPaymentOrderToSuccess(paymentRequest);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
