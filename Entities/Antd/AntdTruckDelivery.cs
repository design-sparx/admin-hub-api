using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdTruckDelivery
    {
        [Key]
        public int Id { get; set; }

        public Guid ShipmentId { get; set; }

        public Guid TruckId { get; set; }

        public Guid CustomerId { get; set; }

        [MaxLength(200)]
        public string CustomerName { get; set; }

        [MaxLength(100)]
        public string DriverName { get; set; }

        [MaxLength(100)]
        public string OriginCity { get; set; }

        [MaxLength(100)]
        public string DestinationCity { get; set; }

        public DateTime ShipmentDate { get; set; }

        public int DeliveryTime { get; set; }

        public decimal ShipmentWeight { get; set; }

        [MaxLength(50)]
        public string DeliveryStatus { get; set; }

        public decimal ShipmentCost { get; set; }

        [MaxLength(50)]
        public string FavoriteColor { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
