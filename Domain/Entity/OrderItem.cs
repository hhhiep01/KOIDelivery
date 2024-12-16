using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        //
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public KoiSize KoiSize { get; set; }
        public int KoiSizeId { get; set; }
        public List<FishDetail>? FishDetails { get; set; }
    }
}
