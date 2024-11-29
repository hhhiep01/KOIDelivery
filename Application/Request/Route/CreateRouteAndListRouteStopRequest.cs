using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Request.RouteStop;

namespace Application.Request.Route
{
    public class CreateRouteAndListRouteStopRequest
    {
        [Required]
        public RouteRequest RouteRequest { get; set; }

        [Required]
        public List<RouteStopRequest> RouteStopRequests { get; set; }
    }
}
