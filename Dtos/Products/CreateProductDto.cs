using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Products;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
    public string Name { get; set; }

    [MaxLength(100, ErrorMessage = "SKU cannot exceed 100 characters")]
    public string Sku { get; set; }

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, 999999999.99, ErrorMessage = "Price must be a positive value")]
    public decimal Price { get; set; }

    [Range(0, 999999999.99, ErrorMessage = "Compare at price must be a positive value")]
    public decimal? CompareAtPrice { get; set; }

    [Range(0, 999999999.99, ErrorMessage = "Cost price must be a positive value")]
    public decimal CostPrice { get; set; }

    [Required(ErrorMessage = "Stock quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be non-negative")]
    public int StockQuantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Low stock threshold must be non-negative")]
    public int LowStockThreshold { get; set; }

    [MaxLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; }

    [MaxLength(100, ErrorMessage = "Brand cannot exceed 100 characters")]
    public string Brand { get; set; }

    [MaxLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
    public string ImageUrl { get; set; }

    public List<string> Tags { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFeatured { get; set; } = false;
}
