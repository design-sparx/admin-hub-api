using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IKanbanTaskService
    {
        Task<ApiResponse<List<KanbanTaskDto>>> GetAllAsync(KanbanTaskQueryParams queryParams);
        Task<KanbanTaskDto?> GetByIdAsync(string id);
        Task<KanbanTaskDto> CreateAsync(KanbanTaskDto taskDto);
        Task<KanbanTaskDto?> UpdateAsync(string id, KanbanTaskDto taskDto);
        Task<bool> DeleteAsync(string id);
    }
}