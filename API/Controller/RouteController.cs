using Application.Interface;
using Application.Request.Route;
using Application.Request.RouteStop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        public IRouteService _service;

        public RouteController(IRouteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRouteAndRouteStopsAsync(CreateRouteAndListRouteStopRequest request)
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
            var result = await _service.AddRouteAsync(request.RouteRequest, request.RouteStopRequests);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRouteAsync()
        {
            var response = await _service.GetAllRouteAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetRouteByDriver{driverId}")]
        public async Task<IActionResult> GetAllRouteByDriverIdAsync(int driverId)
        {
            var response = await _service.GetAllRouteByDriverIdAsync(driverId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteByIdAsync(int id)
        {
            var response = await _service.GetRouteByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRouteByIdAsync(int id)
        {
            var response = await _service.DeleteRouteByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
