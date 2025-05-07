using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
    Task<ProductCategory> GetByIdAsync(Guid id);
    Task CreateAsync(ProductCategory productCategory);
    Task UpdateAsync(ProductCategory productCategory);
    Task DeleteAsync(Guid id);
}