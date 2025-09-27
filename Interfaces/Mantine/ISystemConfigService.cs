using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface ISystemConfigService
    {
        Task<ApiResponse<List<LanguageDto>>> GetLanguagesAsync();
        Task<ApiResponse<List<CountryDto>>> GetCountriesAsync();
        Task<ApiResponse<List<TrafficDto>>> GetTrafficAsync(TrafficQueryParams queryParams);
    }
}