using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(ApplicationDbContext context, ILogger<InvoiceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<InvoiceDto>>> GetAllAsync(InvoiceQueryParams queryParams)
        {
            try
            {
                var query = _context.Invoices.AsQueryable();

                // Apply filters
                if (queryParams.Status.HasValue)
                {
                    query = query.Where(i => i.Status == queryParams.Status.Value);
                }

                if (queryParams.MinAmount.HasValue)
                {
                    query = query.Where(i => i.Amount >= queryParams.MinAmount.Value);
                }

                if (queryParams.MaxAmount.HasValue)
                {
                    query = query.Where(i => i.Amount <= queryParams.MaxAmount.Value);
                }

                if (queryParams.IssueDateFrom.HasValue)
                {
                    query = query.Where(i => i.IssueDate >= queryParams.IssueDateFrom.Value);
                }

                if (queryParams.IssueDateTo.HasValue)
                {
                    query = query.Where(i => i.IssueDate <= queryParams.IssueDateTo.Value);
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "full_name" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.FullName)
                        : query.OrderBy(i => i.FullName),
                    "amount" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.Amount)
                        : query.OrderBy(i => i.Amount),
                    "status" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.Status)
                        : query.OrderBy(i => i.Status),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(i => i.IssueDate)
                        : query.OrderBy(i => i.IssueDate)
                };

                var total = await query.CountAsync();
                var invoices = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var invoicesDto = invoices.Select(i => new InvoiceDto
                {
                    Id = i.Id.ToString(),
                    FullName = i.FullName,
                    Email = i.Email,
                    Address = i.Address,
                    Country = i.Country,
                    Status = i.Status,
                    Amount = i.Amount,
                    IssueDate = i.IssueDate.ToString("yyyy-MM-dd"),
                    Description = i.Description ?? string.Empty,
                    ClientEmail = i.ClientEmail,
                    ClientAddress = i.ClientAddress,
                    ClientCountry = i.ClientCountry,
                    ClientName = i.ClientName,
                    ClientCompany = i.ClientCompany
                }).ToList();

                return new ApiResponse<List<InvoiceDto>>
                {
                    Data = invoicesDto,
                    Meta = new PaginationMeta
                    {
                        Page = queryParams.Page,
                        Limit = queryParams.Limit,
                        Total = total,
                        TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoices data");
                throw;
            }
        }

        public async Task<InvoiceDto?> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var invoice = await _context.Invoices.FindAsync(guidId);
                if (invoice == null) return null;

                return new InvoiceDto
                {
                    Id = invoice.Id.ToString(),
                    FullName = invoice.FullName,
                    Email = invoice.Email,
                    Address = invoice.Address,
                    Country = invoice.Country,
                    Status = invoice.Status,
                    Amount = invoice.Amount,
                    IssueDate = invoice.IssueDate.ToString("yyyy-MM-dd"),
                    Description = invoice.Description ?? string.Empty,
                    ClientEmail = invoice.ClientEmail,
                    ClientAddress = invoice.ClientAddress,
                    ClientCountry = invoice.ClientCountry,
                    ClientName = invoice.ClientName,
                    ClientCompany = invoice.ClientCompany
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice with ID {InvoiceId}", id);
                throw;
            }
        }

        public async Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto)
        {
            try
            {
                var invoice = new Invoices
                {
                    Id = Guid.NewGuid(),
                    FullName = invoiceDto.FullName,
                    Email = invoiceDto.Email,
                    Address = invoiceDto.Address,
                    Country = invoiceDto.Country,
                    Status = invoiceDto.Status,
                    Amount = invoiceDto.Amount,
                    IssueDate = DateTime.Parse(invoiceDto.IssueDate),
                    Description = invoiceDto.Description,
                    ClientEmail = invoiceDto.ClientEmail,
                    ClientAddress = invoiceDto.ClientAddress,
                    ClientCountry = invoiceDto.ClientCountry,
                    ClientName = invoiceDto.ClientName,
                    ClientCompany = invoiceDto.ClientCompany,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                invoiceDto.Id = invoice.Id.ToString();
                return invoiceDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new invoice");
                throw;
            }
        }

        public async Task<InvoiceDto?> UpdateAsync(string id, InvoiceDto invoiceDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var invoice = await _context.Invoices.FindAsync(guidId);
                if (invoice == null) return null;

                invoice.FullName = invoiceDto.FullName;
                invoice.Email = invoiceDto.Email;
                invoice.Address = invoiceDto.Address;
                invoice.Country = invoiceDto.Country;
                invoice.Status = invoiceDto.Status;
                invoice.Amount = invoiceDto.Amount;
                invoice.IssueDate = DateTime.Parse(invoiceDto.IssueDate);
                invoice.Description = invoiceDto.Description;
                invoice.ClientEmail = invoiceDto.ClientEmail;
                invoice.ClientAddress = invoiceDto.ClientAddress;
                invoice.ClientCountry = invoiceDto.ClientCountry;
                invoice.ClientName = invoiceDto.ClientName;
                invoice.ClientCompany = invoiceDto.ClientCompany;
                invoice.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return invoiceDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating invoice with ID {InvoiceId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var invoice = await _context.Invoices.FindAsync(guidId);
                if (invoice == null) return false;

                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice with ID {InvoiceId}", id);
                throw;
            }
        }
    }
}