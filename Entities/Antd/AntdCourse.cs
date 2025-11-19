using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdCourse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string InstructorName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreditHours { get; set; }

        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Prerequisites { get; set; } = string.Empty;

        [MaxLength(500)]
        public string CourseLocation { get; set; } = string.Empty;

        public int TotalLessons { get; set; }

        public int CurrentLessons { get; set; }

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
