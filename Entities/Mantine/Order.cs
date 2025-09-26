using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Mantine
{
    public class Order
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Product { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class OrderStatus
    {
        public const string Pending = "pending";
        public const string Processing = "processing";
        public const string Shipped = "shipped";
        public const string Delivered = "delivered";
        public const string Cancelled = "cancelled";
    }

    public static class PaymentMethod
    {
        public const string CreditCard = "credit_card";
        public const string DebitCard = "debit_card";
        public const string PayPal = "paypal";
        public const string ApplePay = "apple_pay";
        public const string GooglePay = "google_pay";
        public const string Bitcoin = "bitcoin";
        public const string Venmo = "venmo";
        public const string GiftCard = "gift_card";
    }
}