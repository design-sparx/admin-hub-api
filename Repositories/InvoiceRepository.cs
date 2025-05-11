using AdminHubApi.Data;
using AdminHubApi.Entities.Invoice;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public InvoiceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _dbContext.Invoices
            .Include(p => p.CreatedBy)
            .Include(p => p.ModifiedBy)
            .ToListAsync();
    }

    public async Task<Invoice> GetByIdAsync(Guid id)
    {
        return await _dbContext.Invoices
            .Include(p => p.CreatedBy)
            .Include(p => p.ModifiedBy)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Invoice>> GetByUserIdAsync(string userId)
    {
        return await _dbContext.Invoices
            .Include(p => p.CreatedBy)
            .Include(p => p.ModifiedBy)
            .Where(p => p.CreatedBy.Id == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetByOrderIdAsync(string orderId)
    {
        return await _dbContext.Invoices
            .Include(p => p.CreatedBy)
            .Include(p => p.ModifiedBy)
            .Where(p => p.OrderId == Guid.Parse(orderId))
            .ToListAsync();
    }

    public async Task<Invoice> CreateAsync(Invoice invoice)
    {
        invoice.Created = DateTime.UtcNow;
        invoice.Modified = DateTime.UtcNow;

        await _dbContext.Invoices.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();

        return invoice;
    }

    public async Task<Invoice> UpdateAsync(Invoice invoice)
    {
        if (!await ExistsAsync(invoice.Id))
        {
            return null;
        }

        invoice.Modified = DateTime.UtcNow;

        _dbContext.Invoices.Update(invoice);
        await _dbContext.SaveChangesAsync();

        return invoice;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var invoice = await _dbContext.Invoices.FindAsync(id);

        if (await ExistsAsync(id) && invoice != null)
        {
            _dbContext.Invoices.Remove(invoice);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Invoices.AnyAsync(i => i.Id == id);
    }
}