using Application.Interface;
using Application.Request.Fish;
using Application.Request.FishHealth;
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
            var result = await _service.CreateFishQualificationAsync(fish, fish.File);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFishQualificationByIdAsync(int id)
        {
            var resposne = await _service.GetFishQualificationByIdAsync(id);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpPut("Update-fishQualification")]
        public async Task<IActionResult> UpdateFishQualificationAsync(FishQualificationUpdateRequest request)
        {
            var resposne = await _service.UpdateFishQualificationAsync(request);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
    }
}
