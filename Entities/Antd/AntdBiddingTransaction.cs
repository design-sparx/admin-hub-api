using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdBiddingTransaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Image { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ProductId { get; set; } = string.Empty;

        public DateTime TransactionDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Seller { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Buyer { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Profit { get; set; }

        public int Quantity { get; set; }

        [MaxLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string TransactionType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
