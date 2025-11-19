using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdCampaignAdDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("ad_source")]
        public string AdSource { get; set; } = string.Empty;

        [JsonPropertyName("ad_campaign")]
        public string AdCampaign { get; set; } = string.Empty;

        [JsonPropertyName("ad_group")]
        public string AdGroup { get; set; } = string.Empty;

        [JsonPropertyName("ad_type")]
        public string AdType { get; set; } = string.Empty;

        [JsonPropertyName("impressions")]
        public int Impressions { get; set; }

        [JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        [JsonPropertyName("conversions")]
        public int Conversions { get; set; }

        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        [JsonPropertyName("conversion_rate")]
        public decimal ConversionRate { get; set; }

        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }

        [JsonPropertyName("roi")]
        public decimal Roi { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } = string.Empty;
    }

    public class AntdCampaignAdQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? AdSource { get; set; }
        public string? AdCampaign { get; set; }
        public string? AdType { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public decimal? MinRoi { get; set; }
        public string SortBy { get; set; } = "startDate";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdCampaignAdResponse : ApiResponse<AntdCampaignAdDto> { }
    public class AntdCampaignAdListResponse : ApiResponse<List<AntdCampaignAdDto>> { }
    public class AntdCampaignAdCreateResponse : ApiResponse<AntdCampaignAdDto> { }
    public class AntdCampaignAdUpdateResponse : ApiResponse<AntdCampaignAdDto> { }
    public class AntdCampaignAdDeleteResponse : ApiResponse<object> { }
}
