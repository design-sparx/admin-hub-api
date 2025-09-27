using AdminHubApi.Enums.Mantine;
using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Mantine
{
    public class InvoiceDto
    {
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public InvoiceStatus Status { get; set; }
        public decimal Amount { get; set; }

        [JsonPropertyName("issue_date")]
        public string IssueDate { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("client_email")]
        public string ClientEmail { get; set; } = string.Empty;

        [JsonPropertyName("client_address")]
        public string ClientAddress { get; set; } = string.Empty;

        [JsonPropertyName("client_country")]
        public string ClientCountry { get; set; } = string.Empty;

        [JsonPropertyName("client_name")]
        public string ClientName { get; set; } = string.Empty;

        [JsonPropertyName("client_company")]
        public string ClientCompany { get; set; } = string.Empty;
    }

    public class InvoiceQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public InvoiceStatus? Status { get; set; }

        [JsonPropertyName("min_amount")]
        public decimal? MinAmount { get; set; }

        [JsonPropertyName("max_amount")]
        public decimal? MaxAmount { get; set; }

        [JsonPropertyName("issue_date_from")]
        public DateTime? IssueDateFrom { get; set; }

        [JsonPropertyName("issue_date_to")]
        public DateTime? IssueDateTo { get; set; }

        [JsonPropertyName("sort_by")]
        public string SortBy { get; set; } = "issue_date";

        [JsonPropertyName("sort_order")]
        public string SortOrder { get; set; } = "desc";
    }

    public class InvoiceResponse : ApiResponse<InvoiceDto>
    {
    }

    public class InvoiceListResponse : ApiResponse<List<InvoiceDto>>
    {
    }

    public class InvoiceCreateResponse : ApiResponse<InvoiceDto>
    {
    }

    public class InvoiceUpdateResponse : ApiResponse<InvoiceDto>
    {
    }

    public class InvoiceDeleteResponse : ApiResponse<object>
    {
    }
}