using AdminHubApi.Constants;
using AdminHubApi.Dtos.Products;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/products")]
[PermissionAuthorize(Permissions.Products.View)]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProducts()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [PermissionAuthorize(Permissions.Products.Create)]
    public async Task<ActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = createProductDto.Title,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            QuantityInStock = createProductDto.QuantityInStock,
            SKU = createProductDto.SKU,
            ImageUrl = createProductDto.ImageUrl,
            Status = createProductDto.Status,
            CategoryId = createProductDto.CategoryId,
            CreatedById = createProductDto.CreatedById,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow
        };

        var result = await _productService.CreateAsync(product);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, result);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize(Permissions.Products.Edit)]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
    {
        var productResponse = await _productService.GetByIdAsync(id);
    
        if (!productResponse.Succeeded)
        {
            return NotFound(productResponse);
        }
        
        var existingProduct = productResponse.Data;
        existingProduct.Title = updateProductDto.Title;
        existingProduct.Description = updateProductDto.Description ?? string.Empty;
        existingProduct.ModifiedById = updateProductDto.ModifiedById;
        existingProduct.Modified = DateTime.UtcNow;

        await _productService.UpdateAsync(existingProduct);
        
        return Ok(existingProduct);
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize(Permissions.Products.Delete)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var productResponse = await _productService.GetByIdAsync(id);
    
        if (!productResponse.Succeeded)
        {
            return NotFound(productResponse);
        }

        await _productService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProductsByStatus(ProductStatus status)
    {
        var products = await _productService.GetProductsByStatusAsync(status);

        return Ok(products);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProductsByCategory(Guid categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);

        return Ok(products);
    }

    [HttpGet("created-by/{createdById}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCreatedBy(string createdById)
    {
        var products = await _productService.GetProductsByCreatedByAsync(createdById);

        return Ok(products);
    }
}