using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string SKU { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string OwnerId { get; set; }
    public Guid? CategoryId { get; set; }
    public string CategoryName { get; set; }
    public Entities.ProductCategory Category { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string StatusText => Status.ToString();
}