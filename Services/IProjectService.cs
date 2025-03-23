using AdminHubApi.Dtos;
using AdminHubApi.Dtos.Projects;

namespace AdminHubApi.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllProductsAsync();
    Task<ProjectResponseDto> GetProductByIdAsync(Guid id);
    Task<Guid> AddProductAsync(CreateProjectDto projectDto);
    Task UpdateProductAsync(Guid id, ProjectResponseDto projectDto);
    Task DeleteProductAsync(Guid id);
}