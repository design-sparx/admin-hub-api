using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Interfaces.Antd
{
    public interface ITaskService
    {
        Task<ApiResponse<List<TaskDto>>> GetAllAsync(TaskQueryParams queryParams);
        Task<TaskDto?> GetByIdAsync(string id);
        Task<TaskDto> CreateAsync(TaskDto taskDto);
        Task<TaskDto?> UpdateAsync(string id, TaskDto taskDto);
        Task<bool> DeleteAsync(string id);
    }
}
