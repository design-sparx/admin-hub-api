using System.Text.Json.Serialization;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdTruckDeliveryRequestDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pickup_location")]
        public string PickupLocation { get; set; }

        [JsonPropertyName("delivery_location")]
        public string DeliveryLocation { get; set; }

        [JsonPropertyName("delivery_date")]
        public DateTime DeliveryDate { get; set; }

        [JsonPropertyName("delivery_time")]
        public int DeliveryTime { get; set; }

        [JsonPropertyName("truck_type")]
        public string TruckType { get; set; }

        [JsonPropertyName("cargo_weight")]
        public decimal CargoWeight { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("contact_number")]
        public string ContactNumber { get; set; }
    }

    public class AntdTruckDeliveryRequestCreateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pickup_location")]
        public string PickupLocation { get; set; }

        [JsonPropertyName("delivery_location")]
        public string DeliveryLocation { get; set; }

        [JsonPropertyName("delivery_date")]
        public DateTime DeliveryDate { get; set; }

        [JsonPropertyName("delivery_time")]
        public int DeliveryTime { get; set; }

        [JsonPropertyName("truck_type")]
        public string TruckType { get; set; }

        [JsonPropertyName("cargo_weight")]
        public decimal CargoWeight { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("contact_number")]
        public string ContactNumber { get; set; }
    }

    public class AntdTruckDeliveryRequestUpdateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pickup_location")]
        public string PickupLocation { get; set; }

        [JsonPropertyName("delivery_location")]
        public string DeliveryLocation { get; set; }

        [JsonPropertyName("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        [JsonPropertyName("delivery_time")]
        public int? DeliveryTime { get; set; }

        [JsonPropertyName("truck_type")]
        public string TruckType { get; set; }

        [JsonPropertyName("cargo_weight")]
        public decimal? CargoWeight { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("contact_number")]
        public string ContactNumber { get; set; }
    }

    public class AntdTruckDeliveryRequestQueryParams : PaginationParams
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("truck_type")]
        public string TruckType { get; set; }

        [JsonPropertyName("delivery_status")]
        public string DeliveryStatus { get; set; }

        [JsonPropertyName("driver_name")]
        public string DriverName { get; set; }

        [JsonPropertyName("sort_by")]
        public string SortBy { get; set; }

        [JsonPropertyName("sort_order")]
        public string SortOrder { get; set; }
    }

    public class AntdTruckDeliveryRequestListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdTruckDeliveryRequestDto> Data { get; set; }

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; }
    }
}
