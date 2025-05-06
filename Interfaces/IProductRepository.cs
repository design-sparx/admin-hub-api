using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsByStatusAsync(ProductStatus status);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetProductsByOwnerAsync(string ownerId);

}