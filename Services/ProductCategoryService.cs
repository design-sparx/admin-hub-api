using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;

namespace AdminHubApi.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<ApiResponse<IEnumerable<ProductCategoryResponseDto>>> GetAllAsync()
    {
        var productCategories = await _productCategoryRepository.GetAllAsync();

        return new ApiResponse<IEnumerable<ProductCategoryResponseDto>>
        {
            Succeeded = true,
            Data = productCategories.Select(MapToResponseDto),
            Message = "Product categories retrieved",
            Errors = []
        };
    }

    public async Task<ApiResponse<ProductCategoryResponseDto>> GetByIdAsync(Guid id)
    {
        var productCategory = await _productCategoryRepository.GetByIdAsync(id);

        if (productCategory == null) throw new KeyNotFoundException($"Product category with id: {id} was not found");

        return new ApiResponse<ProductCategoryResponseDto>
        {
            Succeeded = true,
            Data = MapToResponseDto(productCategory),
            Message = "Product category retrieved",
            Errors = []
        };
    }

    public async Task<ApiResponse<Guid>> CreateAsync(ProductCategory productCategory)
    {
        await _productCategoryRepository.CreateAsync(productCategory);

        return new ApiResponse<Guid>
        {
            Succeeded = true,
            Data = productCategory.Id,
            Message = "Product category created",
            Errors = []
        };
    }

    public async Task UpdateAsync(ProductCategory productCategory)
    {
        var existingProductCategory = await _productCategoryRepository.GetByIdAsync(productCategory.Id);
        
        if (existingProductCategory == null) throw new KeyNotFoundException($"Product category with id: {productCategory.Id} was not found");
        
        await _productCategoryRepository.UpdateAsync(productCategory);
    }

    public Task DeleteAsync(Guid id)
    {
        var existingProductCategory = _productCategoryRepository.GetByIdAsync(id);
        
        if (existingProductCategory == null) throw new KeyNotFoundException($"Product category with id: {id} was not found");
        
        return _productCategoryRepository.DeleteAsync(id);
    }

    private static ProductCategoryResponseDto MapToResponseDto(ProductCategory productCategory)
    {
        return new ProductCategoryResponseDto
        {
            Id = productCategory.Id,
            Title = productCategory.Title,
            Description = productCategory.Description,
            CreatedAt = productCategory.CreatedAt,
            UpdatedAt = productCategory.UpdatedAt,
        };
    }
}