using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdminHubApi.Services.Mantine
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InvoiceService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InvoiceService(ApplicationDbContext context, ILogger<InvoiceService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<InvoiceListResponse> GetAllAsync(InvoiceQueryParams queryParams)
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

                if (!string.IsNullOrEmpty(queryParams.CreatedById))
                {
                    query = query.Where(i => i.CreatedById == queryParams.CreatedById);
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
                    ClientCompany = i.ClientCompany,
                    CreatedById = i.CreatedById,
                    CreatedByEmail = i.CreatedByEmail,
                    CreatedByName = i.CreatedByName,
                    CreatedAt = i.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAt = i.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();

                return new InvoiceListResponse
                {
                    Succeeded = true,
                    Message = "Invoices retrieved successfully",
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

        public async Task<InvoiceResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new InvoiceResponse { Succeeded = false, Message = "Invalid invoice ID format" };

                var invoice = await _context.Invoices.FindAsync(guidId);
                if (invoice == null)
                    return new InvoiceResponse { Succeeded = false, Message = "Invoice not found" };

                var invoiceDto = new InvoiceDto
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
                    ClientCompany = invoice.ClientCompany,
                    CreatedById = invoice.CreatedById,
                    CreatedByEmail = invoice.CreatedByEmail,
                    CreatedByName = invoice.CreatedByName,
                    CreatedAt = invoice.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAt = invoice.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return new InvoiceResponse
                {
                    Succeeded = true,
                    Message = "Invoice retrieved successfully",
                    Data = invoiceDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice with ID {InvoiceId}", id);
                throw;
            }
        }

        public async Task<InvoiceCreateResponse> CreateAsync(InvoiceDto invoiceDto)
        {
            try
            {
                // Get current user information
                var httpContext = _httpContextAccessor.HttpContext;
                var userId = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous";
                var userEmail = httpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
                var userName = httpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ??
                              httpContext?.User?.FindFirst("name")?.Value;

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
                    CreatedById = userId == "anonymous" ? null : userId,
                    CreatedByEmail = userEmail,
                    CreatedByName = userName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                // Update DTO with creator information and timestamps
                invoiceDto.Id = invoice.Id.ToString();
                invoiceDto.CreatedById = invoice.CreatedById;
                invoiceDto.CreatedByEmail = invoice.CreatedByEmail;
                invoiceDto.CreatedByName = invoice.CreatedByName;
                invoiceDto.CreatedAt = invoice.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                invoiceDto.UpdatedAt = invoice.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");

                return new InvoiceCreateResponse
                {
                    Succeeded = true,
                    Message = "Invoice created successfully",
                    Data = invoiceDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new invoice");
                throw;
            }
        }

        public async Task<InvoiceUpdateResponse> UpdateAsync(string id, InvoiceDto invoiceDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new InvoiceUpdateResponse { Succeeded = false, Message = "Invalid invoice ID format" };

                var invoice = await _context.Invoices.FindAsync(guidId);
                if (invoice == null)
                    return new InvoiceUpdateResponse { Succeeded = false, Message = "Invoice not found" };

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

                // Update DTO with creator information and timestamps
                invoiceDto.CreatedById = invoice.CreatedById;
                invoiceDto.CreatedByEmail = invoice.CreatedByEmail;
                invoiceDto.CreatedByName = invoice.CreatedByName;
                invoiceDto.CreatedAt = invoice.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                invoiceDto.UpdatedAt = invoice.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");

                return new InvoiceUpdateResponse
                {
                    Succeeded = true,
                    Message = "Invoice updated successfully",
                    Data = invoiceDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating invoice with ID {InvoiceId}", id);
                throw;
            }
        }

        public async Task<InvoiceDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new InvoiceDeleteResponse { Succeeded = false, Message = "Invalid invoice ID format" };

                var invoice = await _context.Invoices.FindAsync(guidId);
                if (invoice == null)
                    return new InvoiceDeleteResponse { Succeeded = false, Message = "Invoice not found" };

                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();

                return new InvoiceDeleteResponse
                {
                    Succeeded = true,
                    Message = "Invoice deleted successfully",
                    Data = new { id }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice with ID {InvoiceId}", id);
                throw;
            }
        }
    }
}