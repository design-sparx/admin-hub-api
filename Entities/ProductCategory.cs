using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities;

public class ProductCategory
{
    public Guid Id { get; set; }
    [Required] [MaxLength(100)] public string Title { get; set; }
    [MaxLength(255)] public string Description { get; set; } = String.Empty;
    public ICollection<Product> Products { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; }
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    public string ModifiedById { get; set; }
    public ApplicationUser ModifiedBy { get; set; }
}