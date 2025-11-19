using System.Text.Json.Serialization;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdTruckDeliveryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("shipment_id")]
        public Guid ShipmentId { get; set; }

        [JsonPropertyName("truck_id")]
        public Guid TruckId { get; set; }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("origin_city")]
        public string OriginCity { get; set; }

        [JsonPropertyName("destination_city")]
        public string DestinationCity { get; set; }

        [JsonPropertyName("shipment_date")]
        public DateTime ShipmentDate { get; set; }

        [JsonPropertyName("delivery_time")]
        public int DeliveryTime { get; set; }

        [JsonPropertyName("shipment_weight")]
        public decimal ShipmentWeight { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("shipment_cost")]
        public decimal ShipmentCost { get; set; }

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; }
    }

    public class AntdTruckDeliveryCreateDto
    {
        [JsonPropertyName("shipment_id")]
        public Guid ShipmentId { get; set; }

        [JsonPropertyName("truck_id")]
        public Guid TruckId { get; set; }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("origin_city")]
        public string OriginCity { get; set; }

        [JsonPropertyName("destination_city")]
        public string DestinationCity { get; set; }

        [JsonPropertyName("shipment_date")]
        public DateTime ShipmentDate { get; set; }

        [JsonPropertyName("delivery_time")]
        public int DeliveryTime { get; set; }

        [JsonPropertyName("shipment_weight")]
        public decimal ShipmentWeight { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("shipment_cost")]
        public decimal ShipmentCost { get; set; }

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; }
    }

    public class AntdTruckDeliveryUpdateDto
    {
        [JsonPropertyName("shipment_id")]
        public Guid? ShipmentId { get; set; }

        [JsonPropertyName("truck_id")]
        public Guid? TruckId { get; set; }

        [JsonPropertyName("customer_id")]
        public Guid? CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("origin_city")]
        public string OriginCity { get; set; }

        [JsonPropertyName("destination_city")]
        public string DestinationCity { get; set; }

        [JsonPropertyName("shipment_date")]
        public DateTime? ShipmentDate { get; set; }

        [JsonPropertyName("delivery_time")]
        public int? DeliveryTime { get; set; }

        [JsonPropertyName("shipment_weight")]
        public decimal? ShipmentWeight { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("shipment_cost")]
        public decimal? ShipmentCost { get; set; }

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; }
    }

    public class AntdTruckDeliveryQueryParams : PaginationParams
    {
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("origin_city")]
        public string OriginCity { get; set; }

        [JsonPropertyName("destination_city")]
        public string DestinationCity { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("sort_by")]
        public string SortBy { get; set; }

        [JsonPropertyName("sort_order")]
        public string SortOrder { get; set; }
    }

    public class AntdTruckDeliveryListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdTruckDeliveryDto> Data { get; set; }

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; }
    }
}
