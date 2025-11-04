using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IStatsService
    {
        Task<StatsGridResponse> GetStatsAsync();
        Task<List<StatsDto>> GetAllStatsAsync();
        Task<StatsDto?> GetStatByIdAsync(int id);
        Task<StatsDto> CreateStatAsync(StatsDto statsDto);
        Task<StatsDto?> UpdateStatAsync(int id, StatsDto statsDto);
        Task<bool> DeleteStatAsync(int id);
    }
}