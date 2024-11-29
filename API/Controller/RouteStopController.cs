using Application.Interface;
using Application.Request.RouteStop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteStopController : ControllerBase
    {
        public IRouteStopService _service;

        public RouteStopController(IRouteStopService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRouteStopAsync(RouteStopRequest request)
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
            var result = await _service.AddNewRouteStopAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRouteStopAsync()
        {
            var response = await _service.GetAllRouteStopAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("routeStopByRoute{routeId}")]
        public async Task<IActionResult> GetAllRouteStopByRouteIdAsync(int routeId)
        {
            var response = await _service.GetAllRouteStopByRouteIdAsync(routeId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteStopByIdAsync(int id)
        {
            var response = await _service.GetRouteStopByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRouteStopByIdAsync(UpdateRouteStopRequest request)
        {
            var response = await _service.UpdateRouteStopByIdAsync(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRouteStopByIdAsync(int id)
        {
            var response = await _service.DeleteRouteStopByIdAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }


    }
}
