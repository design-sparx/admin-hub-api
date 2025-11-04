using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Sku { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public decimal? CompareAtPrice { get; set; }

    public decimal CostPrice { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    public int LowStockThreshold { get; set; }

    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(100)]
    public string Brand { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; }

    public string Tags { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFeatured { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public string CreatedBy { get; set; }
    public ApplicationUser CreatedByUser { get; set; }
}
