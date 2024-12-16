using Application.Interface;
using Application.Request.BoxType;
using Application.Request.Fish;
using Application.Request.KoiSize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiSizeController : ControllerBase
    {
        public IKoiSizeService _service;
        public KoiSizeController(IKoiSizeService service)
        {
            _service = service;
        }
        [HttpPost("AddKoiSize")]
        public async Task<IActionResult> AddKoiSizeAsync(KoiSizeRequest koiSizeRequest)
        {
            var result = await _service.AddKoiSizeAsync(koiSizeRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAllKoiSizeAsync")]
        public async Task<IActionResult> GetAllKoiSizeAsync()
        {
            var response = await _service.GetAllKoiSizeAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("DeleteKoiSizeBy{id}")]
        public async Task<IActionResult> DeleteKoiSizeAsync(int id)
        {
            var response = await _service.DeleteKoiSizeAsync(id);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("GetKoiSizeByIdBy{id}")]
        public async Task<IActionResult> GetKoiSizeByIdAsync(int id)
        {
            var resposne = await _service.GetKoiSizeByIdAsync(id);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpPut("UpdateKoiSize")]
        public async Task<IActionResult> UpdateBoxTypehAsync(KoiSizeUpdateRequest request)
        {
            var resposne = await _service.UpdateKoiSizeAsync(request);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
    }
}
