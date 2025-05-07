using AdminHubApi.Dtos.UserManagement;

namespace AdminHubApi.Dtos.ProductCategory;

public class ProductCategoryResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string CreatedById { get; set; }
    public UserDto CreatedBy { get; set; }
    public string ModifiedById { get; set; }
    public UserDto ModifiedBy { get; set; }
    public int ProductCount { get; set; }
}