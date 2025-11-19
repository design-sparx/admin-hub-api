using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdProductDto
    {
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("brand")]
        public string Brand { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity_sold")]
        public int QuantitySold { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("expiration_date")]
        public string ExpirationDate { get; set; } = string.Empty;

        [JsonPropertyName("customer_reviews")]
        public int CustomerReviews { get; set; }

        [JsonPropertyName("average_rating")]
        public decimal AverageRating { get; set; }

        [JsonPropertyName("is_featured")]
        public bool IsFeatured { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class AntdProductQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Category { get; set; }
        public bool? IsFeatured { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinRating { get; set; }
        public string SortBy { get; set; } = "quantitySold";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdCategoryDto
    {
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("product_count")]
        public int ProductCount { get; set; }

        [JsonPropertyName("total_quantity_sold")]
        public int TotalQuantitySold { get; set; }

        [JsonPropertyName("total_revenue")]
        public decimal TotalRevenue { get; set; }
    }

    public class AntdProductResponse : ApiResponse<AntdProductDto>
    {
    }

    public class AntdProductListResponse : ApiResponse<List<AntdProductDto>>
    {
    }

    public class AntdCategoryListResponse : ApiResponse<List<AntdCategoryDto>>
    {
    }

    public class AntdProductCreateResponse : ApiResponse<AntdProductDto>
    {
    }

    public class AntdProductUpdateResponse : ApiResponse<AntdProductDto>
    {
    }

    public class AntdProductDeleteResponse : ApiResponse<object>
    {
    }
}
