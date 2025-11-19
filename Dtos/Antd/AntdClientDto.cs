using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdClientDto
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonPropertyName("purchase_date")]
        public string PurchaseDate { get; set; } = string.Empty;

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
    }

    public class AntdClientQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Email { get; set; }
        public string? ProductName { get; set; }
        public string? Country { get; set; }
        public DateTime? PurchaseDateFrom { get; set; }
        public DateTime? PurchaseDateTo { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public decimal? MaxTotalPrice { get; set; }
        public string SortBy { get; set; } = "purchaseDate";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdClientResponse : ApiResponse<AntdClientDto>
    {
    }

    public class AntdClientListResponse : ApiResponse<List<AntdClientDto>>
    {
    }

    public class AntdClientCreateResponse : ApiResponse<AntdClientDto>
    {
    }

    public class AntdClientUpdateResponse : ApiResponse<AntdClientDto>
    {
    }

    public class AntdClientDeleteResponse : ApiResponse<object>
    {
    }
}
