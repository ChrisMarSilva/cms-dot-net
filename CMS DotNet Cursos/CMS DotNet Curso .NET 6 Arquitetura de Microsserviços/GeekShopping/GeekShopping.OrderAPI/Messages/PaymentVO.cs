using GeekShopping.MessageBus;

namespace GeekShopping.OrderAPI.Messages
{
    public class PaymentVO : BaseMessage
    {
        public long OrderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string ExpiryMonthYear { get; set; } = string.Empty;
        public decimal PurchaseAmount { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
