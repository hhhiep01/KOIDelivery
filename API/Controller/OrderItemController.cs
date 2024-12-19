using Application.Interface;
using Application.Request.Fish;
using Application.Request.OrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        public IOrderItemService _service;
        public OrderItemController(IOrderItemService service)
        {
            _service = service;
        }
        [HttpPost("GetCreateOrderFish")]
        public async Task<IActionResult> CreateOrderFishAsync(OrderItemRequest orderItemRequest)
        {
            var result = await _service.AddOrderItemAsync(orderItemRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllOrderItem")]
        public async Task<IActionResult> GetAllOrderItemAsync()
        {
            var response = await _service.GetAllOrderItemAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
