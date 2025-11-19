using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdOrder
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        [MaxLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        public long TrackingNumber { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }

        [MaxLength(255)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string CustomerName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
