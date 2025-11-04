using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Dtos.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Sku { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public decimal CostPrice { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Tags { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
}

public class ProductResponse : ApiResponse<ProductDto>
{
}

public class ProductListResponse : ApiResponse<List<ProductDto>>
{
}

public class ProductCreateResponse : ApiResponse<ProductDto>
{
}

public class ProductUpdateResponse : ApiResponse<ProductDto>
{
}

public class ProductDeleteResponse : ApiResponse<object>
{
}
