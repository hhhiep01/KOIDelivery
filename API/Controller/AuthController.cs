using Application.Interface;
using Application.Request.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest user)
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
            var result = await _service.RegisterAsync(user);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest user)
        {
            var result = await _service.LoginAsync(user);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("loginDriver")]
        public async Task<IActionResult> LoginForDriver(LoginRequest user)
        {
            var result = await _service.LoginForDriverAsync(user);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Verification")]
        public async Task<IActionResult> Verification(VerificationEmailRequest request)
        {

            var result = await _service.VerifyEmailAsync(request.UserId, request.VerificationCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
