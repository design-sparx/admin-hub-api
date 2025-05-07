using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Products;

public class ProductResponseDto
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
    public ProductCategoryResponseDto Category { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string CreatedById { get; set; }
    public UserDto CreatedBy { get; set; }
    public string ModifiedById { get; set; }
    public UserDto ModifiedBy { get; set; }
}