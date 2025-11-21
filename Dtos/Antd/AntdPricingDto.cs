using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdPricingDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("plan")]
        public string Plan { get; set; } = string.Empty;

        [JsonPropertyName("monthly")]
        public decimal Monthly { get; set; }

        [JsonPropertyName("annually")]
        public decimal Annually { get; set; }

        [JsonPropertyName("savings_caption")]
        public string SavingsCaption { get; set; } = string.Empty;

        [JsonPropertyName("features")]
        public List<string> Features { get; set; } = new();

        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;

        [JsonPropertyName("preferred")]
        public bool Preferred { get; set; }
    }

    public class AntdPricingQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Plan { get; set; }
        public bool? Preferred { get; set; }
        public decimal? MaxMonthly { get; set; }
        public decimal? MaxAnnually { get; set; }
        public string SortBy { get; set; } = "monthly";
        public string SortOrder { get; set; } = "asc";
    }

    public class AntdPricingResponse : ApiResponse<AntdPricingDto>
    {
    }

    public class AntdPricingListResponse : ApiResponse<List<AntdPricingDto>>
    {
    }

    public class AntdPricingCreateResponse : ApiResponse<AntdPricingDto>
    {
    }

    public class AntdPricingUpdateResponse : ApiResponse<AntdPricingDto>
    {
    }

    public class AntdPricingDeleteResponse : ApiResponse<object>
    {
    }
}
