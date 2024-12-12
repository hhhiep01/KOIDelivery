using Application.Interface;
using Application.Request.Feedback;
using Application.Request.Order;
using Application.Request.UserAccount;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Customer")]
        [Route("create-order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(OrderRequest order)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    statusCode = 400,
                    isSuccess = false,
                    errorMessage = string.Join("; ", errors),
                    result = (object)null
                });
            }
            var result = await _service.CreateOrderAsync(order);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderAsync()
        {
            var result = await _service.GetAllOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetOrderByIdAsyncAsync/{id}")]
        public async Task<IActionResult> GetOrderByIdAsyncAsync(int id)
        {
            var result = await _service.GetOrderByIdAsyncAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderByIdAsync(int id)
        {
            var result = await _service.DeleteOrderByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("Customer")]
        public async Task<IActionResult> GetAllUserOrderAsync()
        {
            var result = await _service.GetAllUserOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update-Order-Status-Delivering")]
        public async Task<IActionResult> UpdateStatusOrderToDelivering(int OrderId)
        {
            var result = await _service.UpdateStatusOrderToDelivering(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update-Order-Status-PendingPickUp")]
        public async Task<IActionResult> UpdateStatusOrderToPendingPickUp(int OrderId)
        {
            var result = await _service.UpdateStatusOrderToPendingPickUp(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update-Order-Status-Canceled")]
        public async Task<IActionResult> UpdateStatusOrderToCanceled(UpdateOrderToCancelRequest updateOrderToCancelRequest)
        {
            var result = await _service.UpdateStatusOrderToCanceled(updateOrderToCancelRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update-Order-Status-Completed")]
        public async Task<IActionResult> UpdateStatusOrderToCompleted(int OrderId)
        {
            var result = await _service.UpdateStatusOrderToCompleted(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        /*[HttpPut("Caculate-total-price")]
        public async Task<IActionResult> CaculateTotalPrice(int OrderId)
        {
            var result = await _service.CaculateTotalPrice(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }*/

        [HttpPut("CreateFeedBackAsync")]
        public async Task<IActionResult> CreateFeedBackAsync(FeedbackRequest feedback)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    StatusCode = 400,
                    isSuccess = false,
                    errorMessage = string.Join(", ", errors),
                    result = (object)null
                });
            }
            var result = await _service.CreateFeedBackAsync(feedback);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllProccessingOrder")]
        public async Task<IActionResult> GetAllProccessingOrderAsync()
        {
            var result = await _service.GetAllProccessingOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllPendingPickUpOrderAsync")]
        public async Task<IActionResult> GetAllPendingPickUpOrderAsync()
        {
            var result = await _service.GetAllPendingPickUpOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllDeliveringOrderAsync")]
        public async Task<IActionResult> GetAllDeliveringOrderAsync()
        {
            var result = await _service.GetAllDeliveringOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllCompletedOrderAsync")]
        public async Task<IActionResult> GetAllCompletedOrderAsync()
        {
            var result = await _service.GetAllCompletedOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllCanceledOrderAsync")]
        public async Task<IActionResult> GetAllCanceledOrderAsync()
        {
            var result = await _service.GetAllCanceledOrderAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }

}

