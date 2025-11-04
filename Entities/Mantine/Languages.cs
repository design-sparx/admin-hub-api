using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class Languages
    {
        [Key]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NativeName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}