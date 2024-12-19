using Application.Response.BoxType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.BoxAllocation
{
    public class BoxAllocationResponse
    {
        public int Id { get; set; }
        public int BoxCount { get; set; }
        public BoxTypeResponse BoxType { get; set; }
    }
}
