using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TwilioSettings
    {
        public string AccountSid { get; set; }
        public string ApiKeySid { get; set; }
        public string ApiKeySecret { get; set; }
        public string TwilioPhoneNumber { get; set; }
        public string AuthToken { get; set; }

        public string OutgoingApplicationSid { get; set; }
    }
}
