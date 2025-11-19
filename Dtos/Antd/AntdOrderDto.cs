using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdOrderDto
    {
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; set; } = string.Empty;

        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("order_date")]
        public string OrderDate { get; set; } = string.Empty;

        [JsonPropertyName("shipping_address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("tracking_number")]
        public long TrackingNumber { get; set; }

        [JsonPropertyName("shipping_cost")]
        public decimal ShippingCost { get; set; }

        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; } = string.Empty;
    }

    public class AntdOrderQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Country { get; set; }
        public DateTime? OrderDateFrom { get; set; }
        public DateTime? OrderDateTo { get; set; }
        public string SortBy { get; set; } = "orderDate";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdOrderResponse : ApiResponse<AntdOrderDto> { }
    public class AntdOrderListResponse : ApiResponse<List<AntdOrderDto>> { }
    public class AntdOrderCreateResponse : ApiResponse<AntdOrderDto> { }
    public class AntdOrderUpdateResponse : ApiResponse<AntdOrderDto> { }
    public class AntdOrderDeleteResponse : ApiResponse<object> { }
}
