using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IInvoiceService
    {
        Task<ApiResponse<List<InvoiceDto>>> GetAllAsync(InvoiceQueryParams queryParams);
        Task<InvoiceDto?> GetByIdAsync(string id);
        Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto);
        Task<InvoiceDto?> UpdateAsync(string id, InvoiceDto invoiceDto);
        Task<bool> DeleteAsync(string id);
    }
}