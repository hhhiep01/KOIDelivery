using Application.Interface;
using Application.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        public IUserAccountService _service;
        public UserAccountController(IUserAccountService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfileAsync()
        {
            var resposne = await _service.GetUserProfileAsync();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
        [Authorize]
        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfileAsync(UpdateUserRequest updateUserRequest)
        {
            var result = await _service.UpdateUserProfileAsync(updateUserRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("GetAllAccountAsync")]
        public async Task<IActionResult> GetAllAccountAsync()
        {
            var resposne = await _service.GetAllAccountAsync();
            return resposne.IsSuccess ? Ok(resposne) : BadRequest(resposne);
        }
    }
}
