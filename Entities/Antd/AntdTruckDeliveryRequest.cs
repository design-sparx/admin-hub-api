using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdTruckDeliveryRequest
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string PickupLocation { get; set; }

        [MaxLength(300)]
        public string DeliveryLocation { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int DeliveryTime { get; set; }

        [MaxLength(50)]
        public string TruckType { get; set; }

        public decimal CargoWeight { get; set; }

        [MaxLength(50)]
        public string DeliveryStatus { get; set; }

        [MaxLength(100)]
        public string DriverName { get; set; }

        [MaxLength(50)]
        public string ContactNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
