using Application.Interface;
using Application.Request.Order;
using Application.Request.TransportService;
using DocumentFormat.OpenXml.Office2010.Excel;
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
        [HttpPost("AddTransportLocalService")]
        public async Task<IActionResult> AddTransportLocalServiceAsync(TransportLocalServiceRequest request)
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
            var result = await _service.AddTransportLocalServiceAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("AddTransportDomesticService")]
        public async Task<IActionResult> AddTransportDomesticServiceAsync(TransportServiceRequest request)
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
            var result = await _service.AddTransportDomesticServiceAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("AddTransportInternationalService")]
        public async Task<IActionResult> AddTransportInternarionalServiceAsync(TransportServiceRequest request)
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
            var result = await _service.AddTransportInternaltionalServiceAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTransportServiceAsync()
        {
            var resposne = await _service.GetAllTransportServiceAsync();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransportServiceByIdAsync(int id)
        {
            var resposne = await _service.GetTransportServiceByIdAsync(id);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTransportServiceByIdAsync(TransportServiceUpdateRequest request)
        {
            var resposne = await _service.UpdateTransportServiceByIdAsync(request);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTransportServiceByIdAsync(int id)
        {
            var resposne = await _service.DeleteTransportServiceByIdAsync(id);
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpGet("Local")]
        public async Task<IActionResult> GetTransportServiceLocal()
        {
            var resposne = await _service.GetTransportServiceLocal();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpGet("Domestic")]
        public async Task<IActionResult> GetTransportServiceDomestic()
        {
            var resposne = await _service.GetTransportServiceDomestic();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [HttpGet("International")]
        public async Task<IActionResult> GetTransportServiceInternational()
        {
            var resposne = await _service.GetTransportServiceInternational();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
    }
}
