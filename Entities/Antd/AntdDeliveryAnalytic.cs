using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdDeliveryAnalytic
    {
        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        [MaxLength(50)]
        public string Month { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
