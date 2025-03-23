using AdminHubApi.Dtos;

namespace AdminHubApi.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllProductsAsync();
    Task<ProjectResponseDto> GetProductByIdAsync(Guid id);
    Task AddProductAsync(ProjectResponseDto projectDto);
    Task UpdateProductAsync(Guid id, ProjectResponseDto projectDto);
    Task DeleteProductAsync(Guid id);
}