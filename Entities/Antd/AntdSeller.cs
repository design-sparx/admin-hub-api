using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdSeller
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public decimal SalesVolume { get; set; }

        public decimal TotalSales { get; set; }

        public decimal CustomerSatisfaction { get; set; }

        [MaxLength(100)]
        public string SalesRegion { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
