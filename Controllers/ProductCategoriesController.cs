using AdminHubApi.Constants;
using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/product-categories")]
[PermissionAuthorize(Permissions.ProductCategories.View)]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly ILogger<ProductCategoriesController> _logger;

    public ProductCategoriesController(IProductCategoryService productCategoryService,
        ILogger<ProductCategoriesController> logger)
    {
        _productCategoryService = productCategoryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllCategories()
    {
        var productCategories = await _productCategoryService.GetAllAsync();

        return Ok(productCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategory>> GetCategory(Guid id)
    {
        var productCategories = await _productCategoryService.GetByIdAsync(id);

        if (productCategories == null)
        {
            return NotFound();
        }

        return Ok(productCategories);
    }

    [HttpPost]
    [PermissionAuthorize(Permissions.ProductCategories.Create)]
    public async Task<ActionResult<ProductCategory>> CreateCategory(CreateProductCategoryDto productCategoryDto)
    {
        var productCategory = new ProductCategory
        {
            Id = Guid.NewGuid(),
            Title = productCategoryDto.Title,
            Description = productCategoryDto.Description ?? string.Empty,
            CreatedById = productCategoryDto.CreatedById,
            ModifiedById = productCategoryDto.CreatedById,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow
        };

        var result = await _productCategoryService.CreateAsync(productCategory);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetCategory), new { id = productCategory.Id }, result);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize(Permissions.ProductCategories.Edit)]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateProductCategoryDto updateProductCategoryDto)
    {
        var productCategoryResponse = await _productCategoryService.GetByIdAsync(id);

        if (productCategoryResponse == null)
        {
            return NotFound();
        }

        // Update the existing entity instead of creating a new one
        var existingCategory = productCategoryResponse.Data;
        existingCategory.Title = updateProductCategoryDto.Title;
        existingCategory.Description = updateProductCategoryDto.Description ?? string.Empty;
        existingCategory.ModifiedById = updateProductCategoryDto.ModifiedById;
        existingCategory.Modified = DateTime.UtcNow;

        await _productCategoryService.UpdateAsync(existingCategory);

        return Ok(existingCategory);
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize(Permissions.ProductCategories.Delete)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var productCategoryResponse = await _productCategoryService.GetByIdAsync(id);

        if (productCategoryResponse == null)
        {
            return NotFound();
        }

        await _productCategoryService.DeleteAsync(id);

        return NoContent();
    }
}