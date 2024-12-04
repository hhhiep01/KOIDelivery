using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaculateTotalPriceController : ControllerBase
    {
        public ICaculateTotalPriceService _service;
        public CaculateTotalPriceController(ICaculateTotalPriceService service)
        {
            _service = service;
        }
        [HttpPut("Caculate-total-price-local")]
        public async Task<IActionResult> CaculateTotalPriceLocal(int OrderId)
        {
            var result = await _service.CaculateTotalPriceLocal(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Caculate-total-price-international")]
        public async Task<IActionResult> CaculateTotalPrice(int OrderId)
        {
            var result = await _service.CaculateTotalPriceInternational(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Caculate-total-price-Domestic")]
        public async Task<IActionResult> CaculateTotalPriceDomestic(int OrderId)
        {
            var result = await _service.CaculateTotalPriceDomestic(OrderId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
