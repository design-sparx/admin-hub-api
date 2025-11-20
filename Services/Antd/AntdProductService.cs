using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdProductService : IAntdProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdProductService> _logger;

        public AntdProductService(ApplicationDbContext context, ILogger<AntdProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdProductListResponse> GetAllAsync(AntdProductQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdProducts.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Category))
                    query = query.Where(p => p.Category.ToLower() == queryParams.Category.ToLower());

                if (queryParams.IsFeatured.HasValue)
                    query = query.Where(p => p.IsFeatured == queryParams.IsFeatured.Value);

                if (queryParams.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= queryParams.MinPrice.Value);

                if (queryParams.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= queryParams.MaxPrice.Value);

                if (queryParams.MinRating.HasValue)
                    query = query.Where(p => p.AverageRating >= queryParams.MinRating.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "productname" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.ProductName) : query.OrderBy(p => p.ProductName),
                    "price" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "averagerating" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.AverageRating) : query.OrderBy(p => p.AverageRating),
                    "customerreviews" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.CustomerReviews) : query.OrderBy(p => p.CustomerReviews),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.QuantitySold) : query.OrderBy(p => p.QuantitySold)
                };

                var total = await query.CountAsync();
                var products = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdProductListResponse
                {
                    Success = true,
                    Data = products.Select(MapToDto).ToList(),
                    Message = "Products retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd products");
                throw;
            }
        }

        public async Task<AntdProductResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdProductResponse { Success = false, Message = "Invalid product ID format" };

                var product = await _context.AntdProducts.FindAsync(guidId);
                if (product == null)
                    return new AntdProductResponse { Success = false, Message = "Product not found" };

                return new AntdProductResponse { Success = true, Data = MapToDto(product), Message = "Product retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd product {ProductId}", id);
                throw;
            }
        }

        public async Task<AntdProductListResponse> GetTopProductsAsync(int limit)
        {
            try
            {
                var products = await _context.AntdProducts.OrderByDescending(p => p.QuantitySold).Take(limit).ToListAsync();
                return new AntdProductListResponse { Success = true, Data = products.Select(MapToDto).ToList(), Message = "Top products retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top Antd products");
                throw;
            }
        }

        public async Task<AntdCategoryListResponse> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.AntdProducts
                    .GroupBy(p => p.Category)
                    .Select(g => new AntdCategoryDto
                    {
                        Category = g.Key,
                        ProductCount = g.Count(),
                        TotalQuantitySold = g.Sum(p => p.QuantitySold),
                        TotalRevenue = g.Sum(p => p.Price * p.QuantitySold)
                    })
                    .OrderByDescending(c => c.TotalQuantitySold)
                    .ToListAsync();

                return new AntdCategoryListResponse { Success = true, Data = categories, Message = "Categories retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd categories");
                throw;
            }
        }

        public async Task<AntdProductCreateResponse> CreateAsync(AntdProductDto productDto)
        {
            try
            {
                var product = new AntdProduct
                {
                    Id = Guid.NewGuid(),
                    ProductName = productDto.ProductName,
                    Brand = productDto.Brand,
                    Price = productDto.Price,
                    QuantitySold = productDto.QuantitySold,
                    Category = productDto.Category,
                    ExpirationDate = string.IsNullOrEmpty(productDto.ExpirationDate) ? null : DateTime.Parse(productDto.ExpirationDate).ToUniversalTime(),
                    CustomerReviews = productDto.CustomerReviews,
                    AverageRating = productDto.AverageRating,
                    IsFeatured = productDto.IsFeatured,
                    ImageUrl = productDto.ImageUrl,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdProducts.Add(product);
                await _context.SaveChangesAsync();

                return new AntdProductCreateResponse { Success = true, Data = MapToDto(product), Message = "Product created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd product");
                throw;
            }
        }

        public async Task<AntdProductUpdateResponse> UpdateAsync(string id, AntdProductDto productDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdProductUpdateResponse { Success = false, Message = "Invalid product ID format" };

                var product = await _context.AntdProducts.FindAsync(guidId);
                if (product == null)
                    return new AntdProductUpdateResponse { Success = false, Message = "Product not found" };

                product.ProductName = productDto.ProductName;
                product.Brand = productDto.Brand;
                product.Price = productDto.Price;
                product.QuantitySold = productDto.QuantitySold;
                product.Category = productDto.Category;
                product.ExpirationDate = string.IsNullOrEmpty(productDto.ExpirationDate) ? null : DateTime.Parse(productDto.ExpirationDate).ToUniversalTime();
                product.CustomerReviews = productDto.CustomerReviews;
                product.AverageRating = productDto.AverageRating;
                product.IsFeatured = productDto.IsFeatured;
                product.ImageUrl = productDto.ImageUrl;
                product.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdProductUpdateResponse { Success = true, Data = MapToDto(product), Message = "Product updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd product {ProductId}", id);
                throw;
            }
        }

        public async Task<AntdProductDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdProductDeleteResponse { Success = false, Message = "Invalid product ID format" };

                var product = await _context.AntdProducts.FindAsync(guidId);
                if (product == null)
                    return new AntdProductDeleteResponse { Success = false, Message = "Product not found" };

                _context.AntdProducts.Remove(product);
                await _context.SaveChangesAsync();

                return new AntdProductDeleteResponse { Success = true, Message = "Product deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd product {ProductId}", id);
                throw;
            }
        }

        private static AntdProductDto MapToDto(AntdProduct product)
        {
            return new AntdProductDto
            {
                ProductId = product.Id.ToString(),
                ProductName = product.ProductName,
                Brand = product.Brand,
                Price = product.Price,
                QuantitySold = product.QuantitySold,
                Category = product.Category,
                ExpirationDate = product.ExpirationDate?.ToString("yyyy-MM-dd") ?? string.Empty,
                CustomerReviews = product.CustomerReviews,
                AverageRating = product.AverageRating,
                IsFeatured = product.IsFeatured,
                ImageUrl = product.ImageUrl
            };
        }
    }
}
