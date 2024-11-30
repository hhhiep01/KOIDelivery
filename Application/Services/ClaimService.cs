using Application.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimDTO GetUserClaim()
        {
            var tokenUserId = _httpContextAccessor.HttpContext!.User.FindFirst("UserId");
            var tokenUserRole = _httpContextAccessor.HttpContext!.User.FindFirst("Role");
            var tokenUserName = _httpContextAccessor.HttpContext!.User.FindFirst("FullName");
            if (tokenUserId == null)
            {
                throw new ArgumentNullException("UserId can not be found!");
            }
            var userId = Int32.Parse(tokenUserId?.Value.ToString()!);
            Role userRole = Enum.Parse<Role>(tokenUserRole?.Value.ToString()!);
            var fullName = tokenUserName?.ToString();
            var userClaim = new ClaimDTO
            {
                Role = userRole,
                Id = userId,
                Name = fullName,
            };

            return userClaim;
        }      
       
    }
    public class ClaimDTO
    {
        public int Id { get; set; }
        public Role Role { get; set; }

        public string Name { get; set; }
    }


}
