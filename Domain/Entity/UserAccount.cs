using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class UserAccount : Base
    {
        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsEmailVerified { get; set; } = false;
        public Role Role { get; set; }
        //
        public List<EmailVerification>? EmailVerifications { get; set; }
    }
    public enum Role
    {
        Guest,
        Customer,
        SalesStaff,
        DeliveringStaff,
        Manager
    }
}
