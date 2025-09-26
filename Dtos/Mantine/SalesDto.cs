namespace AdminHubApi.Dtos.Mantine
{
    public class SalesDto
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Revenue { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }

    public class SalesQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Source { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string SortBy { get; set; } = "id";
        public string SortOrder { get; set; } = "asc";
    }
}