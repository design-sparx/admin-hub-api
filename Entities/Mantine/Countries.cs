using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class Countries
    {
        [Key]
        [MaxLength(5)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Continent { get; set; } = string.Empty;

        public long? Population { get; set; }

        public bool IsActive { get; set; } = false;
    }
}