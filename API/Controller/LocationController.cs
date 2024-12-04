using Application.Helper;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IGeoLocationService _service;

        public LocationController(IGeoLocationService service)
        {
            _service = service;
        }

        /*[HttpGet]
        public IActionResult GetLocation()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (ipAddress == null)
            {
                return BadRequest("Khong the lay dia chi IP");
            }

            var location = _service.GetLocationFromIp(ipAddress);
            if (location == null)
            {
                return NotFound("Khong the xac dinh vi tri");
            }

            return Ok(location);
        }*/

        [HttpGet("Geo")]
        public async Task<IActionResult> GetLocation()
        {
            var result = await _service.GetGeoInfo();
            if (result == null) 
            {
                return NotFound("Khong the lay thong tin Geo");
            }

            return Ok(result);
        }
    }
}
