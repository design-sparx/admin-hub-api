﻿using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.ProductCategory;

public class UpdateProductCategoryDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public string ModifiedById { get; set; }
}