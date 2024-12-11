using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class RouteStop: Base
    {
        public int Id { get; set; } 
        public int StopOrder { get; set; } 
        public string Address { get; set; } 
        public RouteStopStatus RouteStatus { get; set; }

        public string RouteStopType { get; set; }
        public Route Route { get; set; }
        public int RouteId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
      

    }
    public enum StopOrder
    {
        First = 1,
        Second = 2,
        Third = 3,
        
    }

    public enum RouteStopStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4
    }

}
