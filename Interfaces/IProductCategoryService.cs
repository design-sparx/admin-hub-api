using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IProductCategoryService
{
    Task<ApiResponse<IEnumerable<ProductCategoryResponseDto>>> GetAllAsync();
    Task<ApiResponse<ProductCategoryResponseDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<Guid>> CreateAsync(ProductCategory productCategory);
    Task UpdateAsync(ProductCategoryResponseDto productCategoryResponseDto);
    Task DeleteAsync(Guid id);
}