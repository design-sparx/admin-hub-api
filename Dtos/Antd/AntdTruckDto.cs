using System.Text.Json.Serialization;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdTruckDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("truck_id")]
        public Guid TruckId { get; set; }

        [JsonPropertyName("make")]
        public string Make { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("mileage")]
        public int Mileage { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("availability")]
        public bool Availability { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("progress")]
        public int Progress { get; set; }
    }

    public class AntdTruckCreateDto
    {
        [JsonPropertyName("truck_id")]
        public Guid TruckId { get; set; }

        [JsonPropertyName("make")]
        public string Make { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("mileage")]
        public int Mileage { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("availability")]
        public bool Availability { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("progress")]
        public int Progress { get; set; }
    }

    public class AntdTruckUpdateDto
    {
        [JsonPropertyName("truck_id")]
        public Guid? TruckId { get; set; }

        [JsonPropertyName("make")]
        public string Make { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("mileage")]
        public int? Mileage { get; set; }

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("availability")]
        public bool? Availability { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("progress")]
        public int? Progress { get; set; }
    }

    public class AntdTruckQueryParams
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public bool? Availability { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class AntdTruckListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdTruckDto> Data { get; set; }

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; }
    }
}
