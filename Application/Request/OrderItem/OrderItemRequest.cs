using Application.Request.KoiSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.OrderItem
{
    public class OrderItemRequest
    {
        public int OrderId { get; set; }
        public List<OrderItemDetail> OrderItemDetails { get; set; }
    }
    public class OrderItemDetail
    {
        public int KoiSizeId { get; set; }
        public int Quantity { get; set; }
    }
}
