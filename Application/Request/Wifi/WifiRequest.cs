using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Wifi
{
    public class WifiRequest
    {
        public bool ConsiderIp { get; set; }

        public List<WifiAccessPoint> WifiAccessPoints { get; set; }
    }

    public class WifiAccessPoint
    {
        public string MacAddress { get; set; }
        public int SignalStrength { get; set; }
        public int SignalToNoiseRatio { get; set; }
    }
}
