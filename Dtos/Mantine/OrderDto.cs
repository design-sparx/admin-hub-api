using AdminHubApi.Enums.Mantine;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Mantine
{
    public class OrderDto
    {
        public string Id { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class OrderQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public OrderStatus? Status { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethod? PaymentMethod { get; set; }

        [JsonPropertyName("date_from")]
        public DateTime? DateFrom { get; set; }

        [JsonPropertyName("date_to")]
        public DateTime? DateTo { get; set; }

        [JsonPropertyName("min_total")]
        public decimal? MinTotal { get; set; }

        [JsonPropertyName("max_total")]
        public decimal? MaxTotal { get; set; }

        [JsonPropertyName("sort_by")]
        public string SortBy { get; set; } = "date";

        [JsonPropertyName("sort_order")]
        public string SortOrder { get; set; } = "desc";
    }
}