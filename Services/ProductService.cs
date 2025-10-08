using AdminHubApi.Data;
using AdminHubApi.Dtos.Products;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IAuditService _auditService;

    public ProductService(ApplicationDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return null;

        return MapToDto(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createDto, string userId)
    {
        var product = new Product
        {
            Name = createDto.Name,
            Sku = createDto.Sku,
            Description = createDto.Description,
            Price = createDto.Price,
            CompareAtPrice = createDto.CompareAtPrice,
            CostPrice = createDto.CostPrice,
            StockQuantity = createDto.StockQuantity,
            LowStockThreshold = createDto.LowStockThreshold,
            Category = createDto.Category,
            Brand = createDto.Brand,
            ImageUrl = createDto.ImageUrl,
            Tags = createDto.Tags != null ? string.Join(",", createDto.Tags) : null,
            IsActive = createDto.IsActive,
            IsFeatured = createDto.IsFeatured,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        await _auditService.LogAsync("Product", product.Id.ToString(), "Create", "/api/products", "POST");

        return MapToDto(product);
    }

    public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateDto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return null;

        product.Name = updateDto.Name;
        product.Sku = updateDto.Sku;
        product.Description = updateDto.Description;
        product.Price = updateDto.Price;
        product.CompareAtPrice = updateDto.CompareAtPrice;
        product.CostPrice = updateDto.CostPrice;
        product.StockQuantity = updateDto.StockQuantity;
        product.LowStockThreshold = updateDto.LowStockThreshold;
        product.Category = updateDto.Category;
        product.Brand = updateDto.Brand;
        product.ImageUrl = updateDto.ImageUrl;
        product.Tags = updateDto.Tags != null ? string.Join(",", updateDto.Tags) : null;
        product.IsActive = updateDto.IsActive;
        product.IsFeatured = updateDto.IsFeatured;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        await _auditService.LogAsync("Product", product.Id.ToString(), "Update", $"/api/products/{id}", "PUT");

        return MapToDto(product);
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        await _auditService.LogAsync("Product", id.ToString(), "Delete", $"/api/products/{id}", "DELETE");

        return true;
    }

    public async Task<List<ProductDto>> GetProductsByCategoryAsync(string category)
    {
        var products = await _context.Products
            .Where(p => p.Category == category)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return products.Select(MapToDto).ToList();
    }

    public async Task<List<ProductDto>> GetLowStockProductsAsync()
    {
        var products = await _context.Products
            .Where(p => p.StockQuantity <= p.LowStockThreshold)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();

        return products.Select(MapToDto).ToList();
    }

    public async Task<List<ProductDto>> GetFeaturedProductsAsync()
    {
        var products = await _context.Products
            .Where(p => p.IsFeatured && p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return products.Select(MapToDto).ToList();
    }

    public async Task<bool> UpdateStockQuantityAsync(Guid id, int quantity)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return false;

        product.StockQuantity = quantity;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        await _auditService.LogAsync("Product", id.ToString(), "UpdateStock", $"/api/products/{id}/stock", "PATCH");

        return true;
    }

    private ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Description = product.Description,
            Price = product.Price,
            CompareAtPrice = product.CompareAtPrice,
            CostPrice = product.CostPrice,
            StockQuantity = product.StockQuantity,
            LowStockThreshold = product.LowStockThreshold,
            Category = product.Category,
            Brand = product.Brand,
            ImageUrl = product.ImageUrl,
            Tags = !string.IsNullOrEmpty(product.Tags) ? product.Tags.Split(',').ToList() : new List<string>(),
            IsActive = product.IsActive,
            IsFeatured = product.IsFeatured,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            CreatedBy = product.CreatedBy
        };
    }
}
