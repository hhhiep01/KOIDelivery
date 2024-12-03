using Application.Request.Feedback;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public OrderService _service;
        public FeedbackController(OrderService service)
        {
            _service = service;
        }
        [HttpPut("CreateFeedBackAsync")]
        public async Task<IActionResult> CreateFeedBackAsync(FeedbackRequest feedback)
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
                    errorMessage = string.Join(", ", errors),
                    result = (object)null
                });
            }
            var result = await _service.CreateFeedBackAsync(feedback);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
