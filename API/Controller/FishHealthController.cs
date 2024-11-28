using Application.Interface;
using Application.Request.FishHealth;
using Application.Request.Order;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishHealthController : ControllerBase
    {
        public IFishHealthService _service;
        public FishHealthController(IFishHealthService service)
        {
            _service = service;
        }
        [HttpPost("create-fishHealth")]
        public async Task<IActionResult> CreateFishHealthAsync(FishHealthRequest fish)
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
            var result = await _service.CreateFishHealthAsync(fish);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get-fish")]
        public async Task<IActionResult> GetAllFishHealthAsync()
        {
            var response = await _service.GetAllFishHealthAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFishHealthAsync(int id)
        {
            var response = await _service.DeleteFishHealthAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

    }
}
