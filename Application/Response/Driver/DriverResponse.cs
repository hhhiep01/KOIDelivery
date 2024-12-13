using Application.Response.UserAccount;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Driver
{
    public class DriverResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public string? CurrentProvince { get; set; }
        public DriverStatus? Status { get; set; }
        public UserProfileResponse UserProfile { get; set; }
    }
}
