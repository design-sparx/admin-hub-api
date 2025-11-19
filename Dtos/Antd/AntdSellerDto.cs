using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdSellerDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

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

        [JsonPropertyName("sales_volume")]
        public decimal SalesVolume { get; set; }

        [JsonPropertyName("total_sales")]
        public decimal TotalSales { get; set; }

        [JsonPropertyName("customer_satisfaction")]
        public decimal CustomerSatisfaction { get; set; }

        [JsonPropertyName("sales_region")]
        public string SalesRegion { get; set; } = string.Empty;
    }

    public class AntdSellerQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? SalesRegion { get; set; }
        public string? Country { get; set; }
        public decimal? MinSalesVolume { get; set; }
        public decimal? MinSatisfaction { get; set; }
        public string SortBy { get; set; } = "totalSales";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdSellerResponse : ApiResponse<AntdSellerDto> { }
    public class AntdSellerListResponse : ApiResponse<List<AntdSellerDto>> { }
    public class AntdSellerCreateResponse : ApiResponse<AntdSellerDto> { }
    public class AntdSellerUpdateResponse : ApiResponse<AntdSellerDto> { }
    public class AntdSellerDeleteResponse : ApiResponse<object> { }
}
