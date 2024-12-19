using Application.Response.KoiSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.OrderItem
{
    public class OrderItemResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public KoiSizeResponse KoiSize { get; set; }
        //public List<OrderItemDetailResponse> OrderItemDetails { get; set; }
    }
    public class OrderItemDetailResponse
    {
        public KoiSizeResponse KoiSize { get; set; }
        public int Quantity { get; set; }
    }
}
