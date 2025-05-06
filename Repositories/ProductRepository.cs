using AdminHubApi.Data;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .ToListAsync();

    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task CreateAsync(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();

    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

    }

    public async Task<IEnumerable<Product>> GetProductsByStatusAsync(ProductStatus status)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .Where(p => p.Status == status)
            .ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsByOwnerAsync(string ownerId)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();

    }
}