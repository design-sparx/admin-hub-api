using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/product-categories")]
[Authorize]
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
    public async Task<ActionResult<ProductCategory>> CreateCategory(CreateProductCategoryDto productCategoryDto)
    {
        var productCategory = new ProductCategory
        {
            Id = Guid.NewGuid(),
            Title = productCategoryDto.Title,
            Description = productCategoryDto.Description,
        };

        var result = await _productCategoryService.CreateAsync(productCategory);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetCategory), new { id = productCategory.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateProductCategoryDto updateProductCategoryDto)
    {
        var productCategoryResponse = await _productCategoryService.GetByIdAsync(id);

        if (!productCategoryResponse.Succeeded)
        {
            return NotFound(productCategoryResponse);
        }

        var productCategory = new ProductCategory
        {
            Id = id,
            Title = updateProductCategoryDto.Description
        };

        await _productCategoryService.UpdateAsync(productCategory);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var productCategoryResponse = await _productCategoryService.GetByIdAsync(id);

        if (!productCategoryResponse.Succeeded)
        {
            return NotFound(productCategoryResponse);
        }

        await _productCategoryService.DeleteAsync(id);

        return NoContent();
    }
}