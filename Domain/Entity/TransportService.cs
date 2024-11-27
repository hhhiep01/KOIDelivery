using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class TransportService : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float BasePrice { get; set; }
        public string Description { get; set; }
        public float PricePerKm { get; set; }
        public float PricePerKg { get; set; }
        public float PricePerAmount { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; }
        //
        public List<UserAccount> UserAccounts { get; set; }
    }
}
