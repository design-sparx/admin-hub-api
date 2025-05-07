using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }

        [Required] [MaxLength(100)] public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; } = String.Empty;

        public ICollection<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}