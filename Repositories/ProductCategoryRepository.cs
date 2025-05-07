using AdminHubApi.Data;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        return await _dbContext.ProductCategories
            .Include(pc => pc.CreatedBy)
            .Include(pc => pc.ModifiedBy)
            .Include(pc => pc.Products)
            .ToListAsync();
    }

    public async Task<ProductCategory> GetByIdAsync(Guid id)
    {
        return await _dbContext.ProductCategories
            .Include(pc => pc.CreatedBy)
            .Include(pc => pc.ModifiedBy)
            .Include(pc => pc.Products)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreateAsync(ProductCategory productCategory)
    {
        productCategory.Created = DateTime.UtcNow;
        productCategory.Modified = DateTime.UtcNow;

        await _dbContext.ProductCategories.AddAsync(productCategory);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductCategory productCategory)
    {
        productCategory.Modified = DateTime.UtcNow;

        _dbContext.ProductCategories.Update(productCategory);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var productCategoryDelete = await _dbContext.ProductCategories.FindAsync(id);

        if (productCategoryDelete != null)
        {
            _dbContext.ProductCategories.Remove(productCategoryDelete);

            await _dbContext.SaveChangesAsync();
        }
    }
}