using System.Text.Json.Serialization;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdDeliveryAnalyticDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AntdDeliveryAnalyticCreateDto
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AntdDeliveryAnalyticUpdateDto
    {
        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AntdDeliveryAnalyticQueryParams
    {
        public string Month { get; set; }
        public string Status { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class AntdDeliveryAnalyticListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdDeliveryAnalyticDto> Data { get; set; }

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; }
    }
}
