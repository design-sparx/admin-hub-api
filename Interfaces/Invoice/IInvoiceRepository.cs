using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Interfaces;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<Invoice> GetByIdAsync(Guid id);
    Task<IEnumerable<Invoice>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Invoice>> GetByOrderIdAsync(string orderId);
    Task<Invoice> CreateAsync(Invoice invoice);
    Task<Invoice> UpdateAsync(Invoice invoice);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}