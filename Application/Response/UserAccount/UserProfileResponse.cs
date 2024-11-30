using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.UserAccount
{
    public class UserProfileResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public bool? IsEmailVerified { get; set; } = false;
        public string? ImgUrl { get; set; }
    }
}
