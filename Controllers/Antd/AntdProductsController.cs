using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/products")]
    [Tags("Antd - Products")]
    [PermissionAuthorize(Permissions.Antd.Products)]
    public class AntdProductsController : AntdBaseController
    {
        private readonly IAntdProductService _productService;

        public AntdProductsController(IAntdProductService productService, ILogger<AntdProductsController> logger)
            : base(logger)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdProductListResponse), 200)]
        public async Task<IActionResult> GetAllProducts([FromQuery] AntdProductQueryParams queryParams)
        {
            try
            {
                var response = await _productService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd products");
                return ErrorResponse("Failed to retrieve products", 500);
            }
        }

        [HttpGet("top")]
        [ProducesResponseType(typeof(AntdProductListResponse), 200)]
        public async Task<IActionResult> GetTopProducts([FromQuery] int limit = 10)
        {
            try
            {
                var response = await _productService.GetTopProductsAsync(limit);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top Antd products");
                return ErrorResponse("Failed to retrieve top products", 500);
            }
        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(AntdCategoryListResponse), 200)]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var response = await _productService.GetCategoriesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd categories");
                return ErrorResponse("Failed to retrieve categories", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdProductResponse), 200)]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var response = await _productService.GetByIdAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd product {ProductId}", id);
                return ErrorResponse("Failed to retrieve product", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdProductCreateResponse), 201)]
        public async Task<IActionResult> CreateProduct([FromBody] AntdProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _productService.CreateAsync(productDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd product");
                return ErrorResponse("Failed to create product", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdProductUpdateResponse), 200)]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] AntdProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _productService.UpdateAsync(id, productDto);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd product {ProductId}", id);
                return ErrorResponse("Failed to update product", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdProductDeleteResponse), 200)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var response = await _productService.DeleteAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd product {ProductId}", id);
                return ErrorResponse("Failed to delete product", 500);
            }
        }
    }
}
