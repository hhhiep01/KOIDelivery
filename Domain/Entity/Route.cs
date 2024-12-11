using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Route: Base
    {
        public int Id { get; set; }
        public RouteStatus RouteStatus { get; set; }
        public DateTime CreateAt { get; set; } 
        public string Notes { get; set; } 
        public DateTime DeliveryStartDate { get; set; }
        public Driver Driver { get; set; }
        public int? DriverId { get; set; }
        public List<RouteStop> RouteStops { get; set; }
    }
    public enum RouteStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4
    }

}
