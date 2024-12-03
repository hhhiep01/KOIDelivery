using System;

namespace Domain.Entity
{
    public class Order : Base
    {
        public int Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Processing;
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public string ReceiverPhone { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public decimal? TotalPrice { get; set; }
        public double? Distance  { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public PaymentMethodEnum PaymentMethod { get; set; }
        
        public int? FeedbackStars { get; set; }
        public string? FeedbackContent { get; set; } = string.Empty;
        public string? ReasonToCancel { get; set; } = string.Empty;
        //
        public UserAccount? UserAccount { get; set; }
        public int? AccountId { get; set; }
        public TransportService TransportService { get; set; }
        public int? TransportServiceId { get; set; }
        public List<OrderFish> OrderFishs { get; set; }
        public List<RouteStop> RouteStops { get; set; }
        public List<Payment> Payments { get; set; }
    }

    public enum OrderStatusEnum
    {
        Processing,
        Delivering,
        Canceled,
        Completed,
        PendingPickUp,
    }

    public enum PaymentMethodEnum
    {
        Cash,
        VnPay,
    }
}
