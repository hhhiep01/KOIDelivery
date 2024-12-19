using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.UserAccount
{
    public class UserRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
