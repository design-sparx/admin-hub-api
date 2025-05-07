using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.ProductCategory;

public class CreateProductCategoryDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public string CreatedById { get; set; }
}