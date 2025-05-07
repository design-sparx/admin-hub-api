namespace AdminHubApi.Dtos.ProductCategory;

public class ProductCategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}