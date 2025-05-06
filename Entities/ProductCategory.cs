using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}