using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Jwt.AccessToken;
using Domain;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Types;
namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        private readonly TwilioSettings _settings;

        public CallController(IOptions<TwilioSettings> settings)
        {
            _settings = settings.Value;
        }
    }
}
