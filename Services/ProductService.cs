
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Products;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;

namespace AdminHubApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Succeeded = true,
                Data = products.Select(MapToResponseDto),
                Message = "Products retrieved",
                Errors = []
            };
        }

        public async Task<ApiResponse<ProductResponseDto>> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) throw new KeyNotFoundException($"Product with id: {id} was not found");

            return new ApiResponse<ProductResponseDto>
            {
                Succeeded = true,
                Data = MapToResponseDto(product),
                Message = "Product retrieved",
                Errors = []
            };
        }

        public async Task<ApiResponse<Guid>> CreateAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
            
            return new ApiResponse<Guid>
            {
                Succeeded = true,
                Data = product.Id,
                Message = "Projects retrieved",
                Errors = []
            };
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);

            if (existingProduct == null) 
                throw new KeyNotFoundException($"Product with id: {product.Id} was not found");

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) 
                throw new KeyNotFoundException($"Product with id: {id} was not found");

            await _productRepository.DeleteAsync(id);
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByStatusAsync(ProductStatus status)
        {
            var products = await _productRepository.GetProductsByStatusAsync(status);

            return new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Succeeded = true,
                Data = products.Select(MapToResponseDto),
                Message = "Products retrieved by status",
                Errors = []
            };
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);

            return new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Succeeded = true,
                Data = products.Select(MapToResponseDto),
                Message = "Products retrieved by category",
                Errors = []
            };
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByOwnerAsync(string ownerId)
        {
            var products = await _productRepository.GetProductsByOwnerAsync(ownerId);

            return new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Succeeded = true,
                Data = products.Select(MapToResponseDto),
                Message = "Products retrieved by owner",
                Errors = []
            };
        }

        // Helper method to map Product entity to ProductResponseDto
        private ProductResponseDto MapToResponseDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                SKU = product.SKU,
                ImageUrl = product.ImageUrl,
                Status = product.Status,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                CategoryId = product.CategoryId,
                OwnerId = product.OwnerId,
            };
        }
    }
}