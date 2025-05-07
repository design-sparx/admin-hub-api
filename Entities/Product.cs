using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities;

public enum ProductStatus
{
    Draft,       // Initial creation, not ready for sale
    Active,      // Available for purchase
    OutOfStock,  // Temporarily unavailable due to no inventory
    Discontinued,// No longer being produced/sold
    Archived     // Hidden from the active catalog but kept for record
}

public class Product
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string SKU { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public ProductStatus Status { get; set; } = ProductStatus.Draft;
    public Guid CategoryId { get; set; }
    public ProductCategory Category { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; }
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    public string ModifiedById { get; set; }
    public ApplicationUser ModifiedBy { get; set; }
}