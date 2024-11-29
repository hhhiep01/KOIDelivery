using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.RouteStop
{
    public class RouteStopRequest
    {
        public int StopOrder { get; set; }
        public string Address { get; set; }
        public int RouteId { get; set; }
        public int OrderId { get; set; }
    }
}
