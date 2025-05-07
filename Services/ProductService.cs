
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Dtos.Products;
using AdminHubApi.Dtos.UserManagement;
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

        public async Task UpdateAsync(ProductResponseDto productResponseDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productResponseDto.Id);

            if (existingProduct == null) 
                throw new KeyNotFoundException($"Product with id: {productResponseDto.Id} was not found");
            
            existingProduct.Title = productResponseDto.Title;
            existingProduct.Description = productResponseDto.Description;
            existingProduct.Price = productResponseDto.Price;
            existingProduct.QuantityInStock = productResponseDto.QuantityInStock;
            existingProduct.SKU = productResponseDto.SKU;
            existingProduct.ImageUrl = productResponseDto.ImageUrl;
            existingProduct.IsActive = productResponseDto.IsActive;
            existingProduct.Status = productResponseDto.Status;
            existingProduct.CategoryId = productResponseDto.CategoryId;
            existingProduct.ModifiedById = productResponseDto.ModifiedById;
            existingProduct.Modified = productResponseDto.Modified;

            await _productRepository.UpdateAsync(existingProduct);
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
                Message = $"Products retrieved by status - {status}",
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
                Message = $"Products retrieved by category - {categoryId}",
                Errors = []
            };
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByCreatedByAsync(string createdById)
        {
            var products = await _productRepository.GetProductsByCreatedByAsync(createdById);

            return new ApiResponse<IEnumerable<ProductResponseDto>>
            {
                Succeeded = true,
                Data = products.Select(MapToResponseDto),
                Message = $"Products retrieved by creator - {createdById}",
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
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Title,
                Category = product.Category != null ?
                    new ProductCategoryResponseDto
                    {
                        Id = product.Category.Id,
                        Title = product.Category.Title,
                        Description = product.Category.Description,
                        CreatedById = product.Category.CreatedById,
                        ModifiedById = product.Category.ModifiedById,
                        Created = product.Category.Created,
                        Modified = product.Category.Modified,
                    } : null,
                Created = product.Created,
                Modified = product.Modified,
                CreatedById = product.CreatedById,
                ModifiedById = product.ModifiedById,
                CreatedBy = product.CreatedBy != null
                    ? new UserDto
                    {
                        Id = product.CreatedBy.Id,
                        UserName = product.CreatedBy.UserName,
                        Email = product.CreatedBy.Email,
                        PhoneNumber = product.CreatedBy.PhoneNumber,
                        EmailConfirmed = product.CreatedBy.EmailConfirmed,
                        PhoneNumberConfirmed = product.CreatedBy.PhoneNumberConfirmed,
                        TwoFactorEnabled = product.CreatedBy.TwoFactorEnabled,
                        LockoutEnabled = product.CreatedBy.LockoutEnabled,
                        LockoutEnd = product.CreatedBy.LockoutEnd
                    }
                    : null,
                ModifiedBy = product.ModifiedBy != null
                    ? new UserDto
                    {
                        Id = product.ModifiedBy.Id,
                        UserName = product.ModifiedBy.UserName,
                        Email = product.ModifiedBy.Email,
                        PhoneNumber = product.ModifiedBy.PhoneNumber,
                        EmailConfirmed = product.ModifiedBy.EmailConfirmed,
                        PhoneNumberConfirmed = product.ModifiedBy.PhoneNumberConfirmed,
                        TwoFactorEnabled = product.ModifiedBy.TwoFactorEnabled,
                        LockoutEnabled = product.ModifiedBy.LockoutEnabled,
                        LockoutEnd = product.ModifiedBy.LockoutEnd
                    }
                    : null,
            };
        }
    }
}