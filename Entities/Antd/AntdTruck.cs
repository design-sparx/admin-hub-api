using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdTruck
    {
        [Key]
        public int Id { get; set; }

        public Guid TruckId { get; set; }

        [MaxLength(100)]
        public string Make { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public int Year { get; set; }

        public int Mileage { get; set; }

        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public bool Availability { get; set; }

        [MaxLength(100)]
        public string Origin { get; set; }

        [MaxLength(100)]
        public string Destination { get; set; }

        public int Progress { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
