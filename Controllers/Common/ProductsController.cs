using AdminHubApi.Dtos.Products;
using AdminHubApi.Interfaces;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Controllers.Common;

[ApiController]
[Route("/api/v1/products")]
[Tags("Products")]
public class ProductsController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        : base(logger)
    {
        _productService = productService;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    [HttpGet]
    [PermissionAuthorize("Products.View")]
    [ProducesResponseType(typeof(ProductListResponse), 200)]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new ProductListResponse { Data = products, Succeeded = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products");
            return ErrorResponse("Failed to retrieve products", 500);
        }
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    [HttpGet("{id}")]
    [PermissionAuthorize("Products.View")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound(new ProductResponse { Succeeded = false, Message = "Product not found" });

            return Ok(new ProductResponse { Data = product, Succeeded = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving product {id}");
            return ErrorResponse("Failed to retrieve product", 500);
        }
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    [HttpPost]
    [PermissionAuthorize("Products.Create")]
    [ProducesResponseType(typeof(ProductCreateResponse), 201)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _productService.CreateProductAsync(createDto, userId);

            return StatusCode(201, new ProductCreateResponse { Data = product, Succeeded = true, Message = "Product created successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return ErrorResponse("Failed to create product", 500);
        }
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    [HttpPut("{id}")]
    [PermissionAuthorize("Products.Update")]
    [ProducesResponseType(typeof(ProductUpdateResponse), 200)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.UpdateProductAsync(id, updateDto);

            if (product == null)
                return NotFound(new ProductUpdateResponse { Succeeded = false, Message = "Product not found" });

            return Ok(new ProductUpdateResponse { Data = product, Succeeded = true, Message = "Product updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating product {id}");
            return ErrorResponse("Failed to update product", 500);
        }
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    [HttpDelete("{id}")]
    [PermissionAuthorize("Products.Delete")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var result = await _productService.DeleteProductAsync(id);

            if (!result)
                return NotFound(new ApiResponse<object> { Succeeded = false, Message = "Product not found" });

            return Ok(new ApiResponse<object> { Data = new { id }, Succeeded = true, Message = "Product deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting product {id}");
            return ErrorResponse("Failed to delete product", 500);
        }
    }

    /// <summary>
    /// Get products by category
    /// </summary>
    [HttpGet("category/{category}")]
    [PermissionAuthorize("Products.View")]
    [ProducesResponseType(typeof(ProductListResponse), 200)]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        try
        {
            var products = await _productService.GetProductsByCategoryAsync(category);
            return Ok(new ProductListResponse { Data = products, Succeeded = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving products for category {category}");
            return ErrorResponse("Failed to retrieve products", 500);
        }
    }

    /// <summary>
    /// Get low stock products
    /// </summary>
    [HttpGet("low-stock")]
    [PermissionAuthorize("Products.View")]
    [ProducesResponseType(typeof(ProductListResponse), 200)]
    public async Task<IActionResult> GetLowStockProducts()
    {
        try
        {
            var products = await _productService.GetLowStockProductsAsync();
            return Ok(new ProductListResponse { Data = products, Succeeded = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving low stock products");
            return ErrorResponse("Failed to retrieve low stock products", 500);
        }
    }

    /// <summary>
    /// Get featured products
    /// </summary>
    [HttpGet("featured")]
    [PermissionAuthorize("Products.View")]
    [ProducesResponseType(typeof(ProductListResponse), 200)]
    public async Task<IActionResult> GetFeaturedProducts()
    {
        try
        {
            var products = await _productService.GetFeaturedProductsAsync();
            return Ok(new ProductListResponse { Data = products, Succeeded = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving featured products");
            return ErrorResponse("Failed to retrieve featured products", 500);
        }
    }

    /// <summary>
    /// Update product stock quantity
    /// </summary>
    [HttpPatch("{id}/stock")]
    [PermissionAuthorize("Products.Update")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    public async Task<IActionResult> UpdateStockQuantity(Guid id, [FromBody] int quantity)
    {
        try
        {
            var result = await _productService.UpdateStockQuantityAsync(id, quantity);

            if (!result)
                return NotFound(new ApiResponse<object> { Succeeded = false, Message = "Product not found" });

            return Ok(new ApiResponse<object> { Data = new { id, quantity }, Succeeded = true, Message = "Stock quantity updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating stock for product {id}");
            return ErrorResponse("Failed to update stock quantity", 500);
        }
    }
}
