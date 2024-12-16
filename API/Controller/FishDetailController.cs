using Application.Interface;
using Application.Request.Fish;
using Application.Request.FishDetail;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{

        [Route("api/[controller]")]
        [ApiController]
        public class FishDetailController : ControllerBase
        {
            public IFishDetailService _service;
            public FishDetailController(IFishDetailService service)
            {
                _service = service;
            }
            [HttpPost("GetCreateFishDetail")]
            public async Task<IActionResult> CreateFishDetailAsync(FishDetailRequest order)
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
                var result = await _service.CreateFishDetailAsync(order);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            [HttpGet("GetAllFishDetail")]
            public async Task<IActionResult> GetAllFishDetailAsync()
            {
                var response = await _service.GetAllFishDetailAsync();
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }

            [HttpDelete("DeleteFishDetailBy{id}")]
            public async Task<IActionResult> DeleteFishDetailAsync(int id)
            {
                var response = await _service.DeleteFishDetailAsync(id);
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }

            [HttpGet("GetFishDetailByIdBy{id}")]
            public async Task<IActionResult> GetFishDetailByIdAsync(int id)
            {
                var resposne = await _service.GetFishDetailByIdAsync(id);
                return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
            }
            [HttpPut("UpdateFishDetail")]
            public async Task<IActionResult> UpdateFishDetailAsync(FishDetailUpdateRequest request)
            {
                var resposne = await _service.UpdateFishDetailAsync(request);
                return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
            }
            [HttpGet("GetAllFishDetailByOrderIdAsync{id}")]
            public async Task<IActionResult> GetAllFishDetailByOrderIdAsync(int id)
            {
                var response = await _service.GetAllFishDetailByOrderIdAsync(id);
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }

        }
    }

