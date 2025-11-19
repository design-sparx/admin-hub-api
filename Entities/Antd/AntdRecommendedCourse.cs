using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdRecommendedCourse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Duration { get; set; }

        [MaxLength(50)]
        public string Level { get; set; } = string.Empty;

        public decimal Price { get; set; }

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Instructor { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        [MaxLength(100)]
        public string CourseLanguage { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public int Lessons { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
