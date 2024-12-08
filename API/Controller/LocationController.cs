using Application.Helper;
using Application.Interface;
using Application.Request.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationRequest request)
        {
            if (request == null || request.Latitude == 0 || request.Longitude == 0) 
            {
                return BadRequest(new
                {
                    message = "Invalid location data"
                });
            }
            var result = await _service.UpdateLocationAsync(id, request);
            _service.ProcessLocation(request);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
