using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Invoice;
using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Interfaces;

public interface IInvoiceService
{
    Task<ApiResponse<IEnumerable<InvoiceResponseDto>>> GetAllAsync();
    Task<ApiResponse<InvoiceResponseDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<InvoiceResponseDto>>> GetByUserIdAsync(string userId);
    Task<ApiResponse<IEnumerable<InvoiceResponseDto>>> GetByOrderIdAsync(string orderId);
    Task<ApiResponse<InvoiceResponseDto>> CreateAsync(Invoice invoice);
    Task<ApiResponse<InvoiceResponseDto>> UpdateAsync(InvoiceResponseDto invoiceResponse);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}