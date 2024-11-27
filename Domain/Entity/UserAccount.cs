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
        public List<Order>? Orders { get; set; }
        /*public int? TransportServiceId { get; set; }
        public TransportService? TransportService { get; set; }*/
        public List<OrderFish>? OrderFishes { get; set; }
        public Driver? Driver { get; set; }
        public int? DriverId { get; set; }

    }
    public enum Role
    {
        Customer,
        Guest,
        SalesStaff,
        DeliveringStaff,
        Manager
    }
}
