using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Products;
using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IProductService
{
    Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetAllAsync();
    Task<ApiResponse<ProductResponseDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<Guid>> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
    Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByStatusAsync(ProductStatus status);
    Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByCategoryAsync(Guid categoryId);
    Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByOwnerAsync(string ownerId);
}