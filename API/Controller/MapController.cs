using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IGoogleMapService _googleMapService;

        public MapController(IGoogleMapService googleMapService)
        {
            _googleMapService = googleMapService;
        }

        [HttpGet("distance")]
        public async Task<IActionResult> GetDistance(string origin, string destination)
        {
            var result = await _googleMapService.GetDistanceAsync(origin, destination);
            if (result != null)
            {
                return Ok(result); 
            }
            return BadRequest("Không thể tính khoảng cách giữa hai địa điểm.");
        }
    }
}
