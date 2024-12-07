using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Driver : Base
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public string? CurrentProvince { get; set; }
        public DriverStatus? Status { get; set; }
        public UserAccount? UserAccount { get; set; }
        public List<Route>? Routes { get; set; }
    }
    public enum DriverStatus
    {
        Available = 1,
        OnRoute = 2,
        Inactive = 3
    }

}
