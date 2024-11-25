using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class EmailVerification
    {
        public int Id { get; set; }
        public string VerificationCode { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        //navigation property
        public int UserId { get; set; }
        public UserAccount User { get; set; }
    }
}
