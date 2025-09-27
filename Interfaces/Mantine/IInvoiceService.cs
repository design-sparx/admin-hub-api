using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IInvoiceService
    {
        Task<InvoiceListResponse> GetAllAsync(InvoiceQueryParams queryParams);
        Task<InvoiceResponse> GetByIdAsync(string id);
        Task<InvoiceCreateResponse> CreateAsync(InvoiceDto invoiceDto);
        Task<InvoiceUpdateResponse> UpdateAsync(string id, InvoiceDto invoiceDto);
        Task<InvoiceDeleteResponse> DeleteAsync(string id);
    }
}