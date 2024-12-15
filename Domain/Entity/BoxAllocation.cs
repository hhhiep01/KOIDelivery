using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BoxAllocation: Base
    {
        public int Id { get; set; }
        public int BoxCount { get; set; }
        //
        public BoxType BoxType { get; set; }
        public int BoxTypeId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
