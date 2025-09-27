using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Mantine
{
    public class LanguageDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("native_name")]
        public string NativeName { get; set; } = string.Empty;

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }

    public class CountryDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
        public long? Population { get; set; }
    }

    public class TrafficDto
    {
        public string Id { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public int Visitors { get; set; }
        public int Pageviews { get; set; }

        [JsonPropertyName("bounce_rate")]
        public decimal BounceRate { get; set; }

        [JsonPropertyName("avg_session_duration")]
        public decimal AvgSessionDuration { get; set; }

        public string Date { get; set; } = string.Empty;
    }

    public class TrafficQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Source { get; set; }

        [JsonPropertyName("date_from")]
        public DateTime? DateFrom { get; set; }

        [JsonPropertyName("date_to")]
        public DateTime? DateTo { get; set; }
    }
}