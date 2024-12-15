using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BoxType : Base
    {
        public int Id { get; set; }
        public string BoxName { get; set; }
        public int MyProperty { get; set; }
        public int Capacity { get; set; }
        public decimal ShippingCost { get; set; }
        //
        public List<BoxAllocation> BoxAllocations { get; set; }
    }
}
