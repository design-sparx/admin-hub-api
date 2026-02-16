using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdEmployeeDto
    {
        [JsonPropertyName("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("middle_name")]
        public string MiddleName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;

        [JsonPropertyName("hire_date")]
        public string HireDate { get; set; } = string.Empty;

        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }
    }

    public class AntdEmployeeQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Role { get; set; }
        public string? Country { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; } = "lastName";
        public string SortOrder { get; set; } = "asc";
    }

    public class AntdEmployeeStatisticsDto
    {
        [JsonPropertyName("total_employees")]
        public int TotalEmployees { get; set; }

        [JsonPropertyName("average_salary")]
        public decimal AverageSalary { get; set; }

        [JsonPropertyName("average_age")]
        public double AverageAge { get; set; }

        [JsonPropertyName("employees_by_country")]
        public Dictionary<string, int> EmployeesByCountry { get; set; } = new();

        [JsonPropertyName("employees_by_role")]
        public Dictionary<string, int> EmployeesByRole { get; set; } = new();
    }

    public class AntdEmployeeResponse : ApiResponse<AntdEmployeeDto>
    {
    }

    public class AntdEmployeeListResponse : ApiResponse<List<AntdEmployeeDto>>
    {
    }

    public class AntdEmployeeStatisticsResponse : ApiResponse<AntdEmployeeStatisticsDto>
    {
    }

    public class AntdEmployeeCreateResponse : ApiResponse<AntdEmployeeDto>
    {
    }

    public class AntdEmployeeUpdateResponse : ApiResponse<AntdEmployeeDto>
    {
    }

    public class AntdEmployeeDeleteResponse : ApiResponse<object>
    {
    }
}
