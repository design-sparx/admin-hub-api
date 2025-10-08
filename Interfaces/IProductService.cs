using AdminHubApi.Dtos.Products;

namespace AdminHubApi.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid id);
    Task<ProductDto> CreateProductAsync(CreateProductDto createDto, string userId);
    Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateDto);
    Task<bool> DeleteProductAsync(Guid id);
    Task<List<ProductDto>> GetProductsByCategoryAsync(string category);
    Task<List<ProductDto>> GetLowStockProductsAsync();
    Task<List<ProductDto>> GetFeaturedProductsAsync();
    Task<bool> UpdateStockQuantityAsync(Guid id, int quantity);
}
