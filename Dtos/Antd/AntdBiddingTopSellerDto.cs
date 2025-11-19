using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdBiddingTopSellerDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("artist")]
        public string Artist { get; set; } = string.Empty;

        [JsonPropertyName("volume")]
        public int Volume { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("owners_count")]
        public int OwnersCount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("creation_date")]
        public string CreationDate { get; set; } = string.Empty;

        [JsonPropertyName("edition")]
        public int Edition { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("owner")]
        public string Owner { get; set; } = string.Empty;

        [JsonPropertyName("collection")]
        public string Collection { get; set; } = string.Empty;

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
    }

    public class AntdBiddingTopSellerQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Collection { get; set; }
        public string? Artist { get; set; }
        public bool? Verified { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SortBy { get; set; } = "volume";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdBiddingTopSellerResponse : ApiResponse<AntdBiddingTopSellerDto> { }
    public class AntdBiddingTopSellerListResponse : ApiResponse<List<AntdBiddingTopSellerDto>> { }
    public class AntdBiddingTopSellerCreateResponse : ApiResponse<AntdBiddingTopSellerDto> { }
    public class AntdBiddingTopSellerUpdateResponse : ApiResponse<AntdBiddingTopSellerDto> { }
    public class AntdBiddingTopSellerDeleteResponse : ApiResponse<object> { }
}
