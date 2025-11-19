using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdBiddingTransactionDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;

        [JsonPropertyName("transaction_date")]
        public string TransactionDate { get; set; } = string.Empty;

        [JsonPropertyName("seller")]
        public string Seller { get; set; } = string.Empty;

        [JsonPropertyName("buyer")]
        public string Buyer { get; set; } = string.Empty;

        [JsonPropertyName("purchase_price")]
        public decimal PurchasePrice { get; set; }

        [JsonPropertyName("sale_price")]
        public decimal SalePrice { get; set; }

        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("shipping_address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("transaction_type")]
        public string TransactionType { get; set; } = string.Empty;
    }

    public class AntdBiddingTransactionQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? TransactionType { get; set; }
        public string? Seller { get; set; }
        public string? Buyer { get; set; }
        public string? Country { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string SortBy { get; set; } = "transactiondate";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdBiddingTransactionResponse : ApiResponse<AntdBiddingTransactionDto> { }
    public class AntdBiddingTransactionListResponse : ApiResponse<List<AntdBiddingTransactionDto>> { }
    public class AntdBiddingTransactionCreateResponse : ApiResponse<AntdBiddingTransactionDto> { }
    public class AntdBiddingTransactionUpdateResponse : ApiResponse<AntdBiddingTransactionDto> { }
    public class AntdBiddingTransactionDeleteResponse : ApiResponse<object> { }
}
