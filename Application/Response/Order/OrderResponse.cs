using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Order
{
    public class OrderResponse
    {
        public DateTime CreatedAt { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public string ReceiverPhone { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; } = string.Empty;
        public PaymentMethodEnum PaymentMethod { get; set; }
        public string FromProvince { get; set; } = string.Empty;
        public string ToProvince { get; set; } = string.Empty;
        public float FeedbackStars { get; set; }
        public string FeedbackContent { get; set; } = string.Empty;
        public string ReasonToCancel { get; set; } = string.Empty;
    }
}
