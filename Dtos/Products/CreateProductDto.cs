using System.ComponentModel.DataAnnotations;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Products;

public class CreateProductDto
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int QuantityInStock { get; set; }
    public string SKU { get; set; }
    public string ImageUrl { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Draft;
    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    public string CreatedById { get; set; }
}