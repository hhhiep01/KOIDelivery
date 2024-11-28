using Application.Interface;
using Application.Request.FishHealth;
using Application.Request.FishQualification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishQualificationController : ControllerBase
    {
        public IFishQualificationService _service;
        public FishQualificationController(IFishQualificationService service)
        {
            _service = service;
        }
        [HttpPost("create-fishQualification")]
        public async Task<IActionResult> CreateFishQualificationAsync(FishQualificationRequest fish)
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
            var result = await _service.CreateFishQualificationAsync(fish);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get-fish")]
        public async Task<IActionResult> GetAllFishQualificationAsync()
        {
            var response = await _service.GetAllFishQualificationAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFishQualificationAsync(int id)
        {
            var response = await _service.DeleteFishQualificationAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
