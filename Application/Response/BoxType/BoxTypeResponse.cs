using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.BoxType
{
    public class BoxTypeResponse
    {
        public int Id { get; set; }
        public string BoxName { get; set; }
        public int Capacity { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
