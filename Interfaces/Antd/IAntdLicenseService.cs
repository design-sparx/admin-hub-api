using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdLicenseService
    {
        Task<AntdLicenseListResponse> GetAllAsync(AntdLicenseQueryParams queryParams);
        Task<AntdLicenseResponse> GetByIdAsync(string id);
        Task<AntdLicenseResponse> GetByTitleAsync(string title);
        Task<AntdLicenseCreateResponse> CreateAsync(AntdLicenseDto licenseDto);
        Task<AntdLicenseUpdateResponse> UpdateAsync(string id, AntdLicenseDto licenseDto);
        Task<AntdLicenseDeleteResponse> DeleteAsync(string id);
    }
}
