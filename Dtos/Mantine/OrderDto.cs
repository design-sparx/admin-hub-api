namespace AdminHubApi.Dtos.Mantine
{
    public class OrderDto
    {
        public string Id { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class OrderQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }
        public string SortBy { get; set; } = "date";
        public string SortOrder { get; set; } = "desc";
    }
}