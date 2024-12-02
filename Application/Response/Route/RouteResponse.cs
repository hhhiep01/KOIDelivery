using Application.Response.RouteStop;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Route
{
    public class RouteResponse
    {
        public int Id { get; set; }
        public RouteStatus RouteStatus { get; set; }
        public string Notes { get; set; }
        public int? DriverId { get; set; }
        public List<RouteStopResponse> RouteStops { get; set; } 
    }
}
