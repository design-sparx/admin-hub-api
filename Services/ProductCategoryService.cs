﻿using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.ProductCategory;
using AdminHubApi.Dtos.UserManagement;
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

    public async Task UpdateAsync(ProductCategoryResponseDto productCategoryResponseDto)
    {
        var existingProductCategory = await _productCategoryRepository.GetByIdAsync(productCategoryResponseDto.Id);

        if (existingProductCategory == null)
            throw new KeyNotFoundException($"Product category with id: {productCategoryResponseDto.Id} was not found");

        existingProductCategory.Title = productCategoryResponseDto.Title;
        existingProductCategory.Description = productCategoryResponseDto.Description;
        existingProductCategory.ModifiedById = productCategoryResponseDto.ModifiedById;
        existingProductCategory.Modified = productCategoryResponseDto.Modified;

        await _productCategoryRepository.UpdateAsync(existingProductCategory);
    }

    public async Task DeleteAsync(Guid id)
    {
        var productCategory = await _productCategoryRepository.GetByIdAsync(id);

        if (productCategory == null)
            throw new KeyNotFoundException($"Product category with id: {id} was not found");

        await _productCategoryRepository.DeleteAsync(id);
    }

    private static ProductCategoryResponseDto MapToResponseDto(ProductCategory productCategory)
    {
        return new ProductCategoryResponseDto
        {
            Id = productCategory.Id,
            Title = productCategory.Title,
            Description = productCategory.Description,
            Created = productCategory.Created,
            Modified = productCategory.Modified,
            CreatedById = productCategory.CreatedById,
            ModifiedById = productCategory.ModifiedById,
            CreatedBy = productCategory.CreatedBy != null
                ? new UserDto
                {
                    Id = productCategory.CreatedBy.Id,
                    UserName = productCategory.CreatedBy.UserName,
                    Email = productCategory.CreatedBy.Email,
                    PhoneNumber = productCategory.CreatedBy.PhoneNumber,
                    EmailConfirmed = productCategory.CreatedBy.EmailConfirmed,
                    PhoneNumberConfirmed = productCategory.CreatedBy.PhoneNumberConfirmed,
                    TwoFactorEnabled = productCategory.CreatedBy.TwoFactorEnabled,
                    LockoutEnabled = productCategory.CreatedBy.LockoutEnabled,
                    LockoutEnd = productCategory.CreatedBy.LockoutEnd
                }
                : null,
            ModifiedBy = productCategory.ModifiedBy != null
                ? new UserDto
                {
                    Id = productCategory.ModifiedBy.Id,
                    UserName = productCategory.ModifiedBy.UserName,
                    Email = productCategory.ModifiedBy.Email,
                    PhoneNumber = productCategory.ModifiedBy.PhoneNumber,
                    EmailConfirmed = productCategory.ModifiedBy.EmailConfirmed,
                    PhoneNumberConfirmed = productCategory.ModifiedBy.PhoneNumberConfirmed,
                    TwoFactorEnabled = productCategory.ModifiedBy.TwoFactorEnabled,
                    LockoutEnabled = productCategory.ModifiedBy.LockoutEnabled,
                    LockoutEnd = productCategory.ModifiedBy.LockoutEnd
                }
                : null,
            ProductCount = productCategory.Products?.Count ?? 0
        };
    }
}