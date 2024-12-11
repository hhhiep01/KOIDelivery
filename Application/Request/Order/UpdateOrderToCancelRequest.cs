using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Order
{
    public class UpdateOrderToCancelRequest
    {
        public int OrderId { get; set; }
        public string ReasonToCancel { get; set; }
    }
}
