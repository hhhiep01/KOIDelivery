using Application.Interface;
using Application.Request.Order;
using Application.Request.TransportService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportServiceController : ControllerBase
    {
        public ITransportServiceService _service;
        public TransportServiceController(ITransportServiceService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(TransportServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    statusCode = 400,
                    isSuccess = false,
                    errorMessage = string.Join("; ", errors),
                    result = (object)null
                });
            }
            var result = await _service.AddTransportServiceAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTransportServiceAsync()
        {
            var resposne = await _service.GetAllTransportServiceAsync();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTransportServiceByIdAsync(int id)
        {
            var resposne = await _service.DeleteTransportServiceByIdAsync(id);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
    }
}
