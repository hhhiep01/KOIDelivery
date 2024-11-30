using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.RouteStop
{
    public class RouteStopResponse
    {
        public int Id { get; set; }
        public int StopOrder { get; set; }
        public string Address { get; set; }
        public RouteStopStatus RouteStopStatus { get; set; }
        public int RouteId { get; set; }
        public int OrderId { get; set; }
    }
}
