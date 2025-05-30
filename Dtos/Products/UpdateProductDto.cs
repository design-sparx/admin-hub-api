﻿using System.ComponentModel.DataAnnotations;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Products;

public class UpdateProductDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string SKU { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public ProductStatus Status { get; set; }
    public Guid CategoryId { get; set; }
    [Required] 
    public string ModifiedById { get; set; }
}