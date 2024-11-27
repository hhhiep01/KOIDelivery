using System;

namespace Domain.Entity
{
    public class Order 
    {
        public int Id { get; set; }
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
        public DateTime UpdatedAt { get; set; }
        public float FeedbackStars { get; set; }
        public string FeedbackContent { get; set; } = string.Empty;
        public string ReasonToCancel { get; set; } = string.Empty;
        //
        public UserAccount? UserAccount { get; set; }
        public int? AccountId { get; set; }
        public int? TransportServiceId { get; set; }
        public int? PaymentId { get; set; }
    }

    public enum OrderStatusEnum
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Canceled
    }

    public enum PaymentMethodEnum
    {
        CashOnDelivery,
        CreditCard,
        BankTransfer,
        DigitalWallet
    }
}
