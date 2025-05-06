using AdminHubApi.Dtos.Product;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        var productDtos = products.Select(MapToDto);

        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(MapToDto(product));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
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
            OwnerId = User.FindFirst("sub")?.Value ?? User.Identity.Name
        };

        await _productRepository.CreateAsync(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, MapToDto(product));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        // Check if the current user is the owner or has admin rights
        var currentUserId = User.FindFirst("sub")?.Value ?? User.Identity.Name;

        if (product.OwnerId != currentUserId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        // Update the product properties
        product.Title = updateProductDto.Title;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.QuantityInStock = updateProductDto.QuantityInStock;
        product.SKU = updateProductDto.SKU;
        product.ImageUrl = updateProductDto.ImageUrl;
        product.IsActive = updateProductDto.IsActive;
        product.Status = updateProductDto.Status;
        product.CategoryId = updateProductDto.CategoryId;

        await _productRepository.UpdateAsync(product);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        // Check if the current user is the owner or has admin rights
        var currentUserId = User.FindFirst("sub")?.Value ?? User.Identity.Name;

        if (product.OwnerId != currentUserId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        await _productRepository.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByStatus(ProductStatus status)
    {
        var products = await _productRepository.GetProductsByStatusAsync(status);
        var productDtos = products.Select(MapToDto);

        return Ok(productDtos);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(Guid categoryId)
    {
        var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
        var productDtos = products.Select(MapToDto);

        return Ok(productDtos);
    }

    [HttpGet("owner/{ownerId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByOwner(string ownerId)
    {
        var products = await _productRepository.GetProductsByOwnerAsync(ownerId);
        var productDtos = products.Select(MapToDto);

        return Ok(productDtos);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock,
            SKU = product.SKU,
            ImageUrl = product.ImageUrl,
            IsActive = product.IsActive,
            Status = product.Status,
            CreatedDate = product.CreatedDate,
            OwnerId = product.OwnerId,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            LastUpdated = product.LastUpdated
        };
    }
}