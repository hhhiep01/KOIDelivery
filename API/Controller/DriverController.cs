using Application.Interface;
using Application.Request.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreateNewDriver")]
        public async Task<IActionResult> CreateDriverAsync(DriverRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    statusCode = 400,
                    isSuccess = false,
                    errorMessage = string.Join(", ", errors),
                    result = (object)null
                });
            }
            var result = await _service.AddNewDriverAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllDrivers")]
        public async Task<IActionResult> GetAllDriverAsync()
        {
            var response = await _service.GetAllDriverAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetDriverBy{id}")]
        public async Task<IActionResult> GetDriverByIdAsync(int id)
        {
            var response = await _service.GetDriverByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("UpdateDriverById")]
        public async Task<IActionResult> UpdateDriverByIdAsync(UpdateDriverRequest request)
        {
            var response = await _service.UpdateDriverByIdAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("DeleteDriverById")]
        public async Task<IActionResult> DeleteDriverByIdAsync(int id)
        {
            var response = await _service.DeleteDriverByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetDriverLocation/{driverId}")]
        public async Task<IActionResult> GetDriverLocation(int driverId)
        {
            var response = await _service.GetCurrentDriverLocationAsync(driverId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
