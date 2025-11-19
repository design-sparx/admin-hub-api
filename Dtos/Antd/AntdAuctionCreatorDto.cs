using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdAuctionCreatorDto
    {
        [JsonPropertyName("creator_id")]
        public string CreatorId { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = string.Empty;

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;

        [JsonPropertyName("sales_count")]
        public int SalesCount { get; set; }

        [JsonPropertyName("total_sales")]
        public string TotalSales { get; set; } = string.Empty;
    }

    public class AntdAuctionCreatorQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Country { get; set; }
        public string? FavoriteColor { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? MinSalesCount { get; set; }
        public string SortBy { get; set; } = "salescount";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdAuctionCreatorResponse : ApiResponse<AntdAuctionCreatorDto> { }
    public class AntdAuctionCreatorListResponse : ApiResponse<List<AntdAuctionCreatorDto>> { }
    public class AntdAuctionCreatorCreateResponse : ApiResponse<AntdAuctionCreatorDto> { }
    public class AntdAuctionCreatorUpdateResponse : ApiResponse<AntdAuctionCreatorDto> { }
    public class AntdAuctionCreatorDeleteResponse : ApiResponse<object> { }
}
