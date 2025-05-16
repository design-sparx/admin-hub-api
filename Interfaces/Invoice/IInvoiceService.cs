using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Invoice;
using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Interfaces;

public interface IInvoiceService
{
    Task<ApiResponse<IEnumerable<InvoiceDto>>> GetAllAsync();
    Task<ApiResponse<InvoiceDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<InvoiceDto>>> GetByUserIdAsync(string userId);
    Task<ApiResponse<IEnumerable<InvoiceDto>>> GetByOrderIdAsync(string orderId);
    Task<ApiResponse<InvoiceDto>> CreateAsync(Invoice invoice);
    Task<ApiResponse<InvoiceDto>> UpdateAsync(InvoiceDto invoice);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}