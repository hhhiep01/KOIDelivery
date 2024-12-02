using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.RouteStop
{
    public class UpdateStopOrderAndStatusRequest
    {
        public List<RouteStopRequest> RouteStops { get; set; }
        public int CurrentStopOrder { get; set; }
        public RouteStatus RouteStatus { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
    }
}
