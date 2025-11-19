using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdExam
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Course { get; set; } = string.Empty;

        [MaxLength(50)]
        public string CourseCode { get; set; } = string.Empty;

        public DateTime ExamDate { get; set; }

        public int ExamTime { get; set; }

        public int ExamDuration { get; set; }

        public int ExamScore { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
