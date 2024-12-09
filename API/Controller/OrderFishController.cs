﻿using Application.Interface;
using Application.Request.Fish;
using Application.Request.TransportService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderFishController : ControllerBase
    {
        public IOrderFishService _service;
        public OrderFishController(IOrderFishService service)
        {
            _service = service;
        }
        [HttpPost("GetCreateOrderFish")]
        public async Task<IActionResult> CreateOrderFishAsync(OrderFishRequest order)
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
                    errorMessage = string.Join("; ", errors),
                    result = (object)null
                });
            }
            var result = await _service.CreateOrderFishAsync(order);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllOrderFish")]
        public async Task<IActionResult> GetAllOrderFishAsync()
        {
            var response = await _service.GetAllOrderFishAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        
        [HttpDelete("DeleteOrderFishBy{id}")]
        public async Task<IActionResult> DeleteOrderFishAsync(int id)
        {
            var response =  await _service.DeleteOrderFishAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetOrderFishByIdBy{id}")]
        public async Task<IActionResult> GetOrderFishByIdAsync(int id)
        {
            var resposne = await _service.GetOrderFishByIdAsync(id);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpPut("UpdateOrderFish")]
        public async Task<IActionResult> UpdateOrderFishAsync(OrderFishUpdateRequest request)
        {
            var resposne = await _service.UpdateOrderFishAsync(request);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpGet("GetAllOrderFishByOrderIdAsync{id}")]
        public async Task<IActionResult> GetAllOrderFishByOrderIdAsync(int id)
        {
            var response = await _service.GetAllOrderFishByOrderIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

    }
}
