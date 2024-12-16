using Application.Interface;
using Application.Request.BoxType;
using Application.Request.KoiSize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxTypeController : ControllerBase
    {
        public IBoxTypeService _service;
        public BoxTypeController(IBoxTypeService service)
        {
            _service = service;
        }
        [HttpPost("AddBoxTypeRequestAsync")]
        public async Task<IActionResult> AddBoxTypeRequestAsync(BoxTypeRequest boxTypeRequest)
        {
            var result = await _service.AddBoxTypeRequestAsync(boxTypeRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAlBoxTypeAsync")]
        public async Task<IActionResult> GetAlBoxTypeAsync()
        {
            var response = await _service.GetAlBoxTypeAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
