using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdLicenseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }

    public class AntdLicenseQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Title { get; set; }
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; } = "title";
        public string SortOrder { get; set; } = "asc";
    }

    public class AntdLicenseResponse : ApiResponse<AntdLicenseDto>
    {
    }

    public class AntdLicenseListResponse : ApiResponse<List<AntdLicenseDto>>
    {
    }

    public class AntdLicenseCreateResponse : ApiResponse<AntdLicenseDto>
    {
    }

    public class AntdLicenseUpdateResponse : ApiResponse<AntdLicenseDto>
    {
    }

    public class AntdLicenseDeleteResponse : ApiResponse<object>
    {
    }
}
