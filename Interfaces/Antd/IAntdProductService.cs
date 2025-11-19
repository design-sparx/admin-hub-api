using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdProductService
    {
        Task<AntdProductListResponse> GetAllAsync(AntdProductQueryParams queryParams);
        Task<AntdProductResponse> GetByIdAsync(string id);
        Task<AntdProductListResponse> GetTopProductsAsync(int limit);
        Task<AntdCategoryListResponse> GetCategoriesAsync();
        Task<AntdProductCreateResponse> CreateAsync(AntdProductDto productDto);
        Task<AntdProductUpdateResponse> UpdateAsync(string id, AntdProductDto productDto);
        Task<AntdProductDeleteResponse> DeleteAsync(string id);
    }
}
