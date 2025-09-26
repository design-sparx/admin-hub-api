using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class SalesService : ISalesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SalesService> _logger;

        public SalesService(ApplicationDbContext context, ILogger<SalesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<SalesDto>>> GetAllAsync(SalesQueryParams queryParams)
        {
            try
            {
                var query = _context.Sales.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Source))
                {
                    query = query.Where(s => s.Source.Contains(queryParams.Source));
                }

                if (queryParams.MinValue.HasValue)
                {
                    query = query.Where(s => s.Value >= queryParams.MinValue.Value);
                }

                if (queryParams.MaxValue.HasValue)
                {
                    query = query.Where(s => s.Value <= queryParams.MaxValue.Value);
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "source" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.Source)
                        : query.OrderBy(s => s.Source),
                    "revenue" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.RevenueAmount)
                        : query.OrderBy(s => s.RevenueAmount),
                    "value" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.Value)
                        : query.OrderBy(s => s.Value),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.Id)
                        : query.OrderBy(s => s.Id)
                };

                var total = await query.CountAsync();
                var sales = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var salesDto = sales.Select(s => new SalesDto
                {
                    Id = s.Id,
                    Source = s.Source,
                    Revenue = s.RevenueFormatted,
                    Value = s.Value
                }).ToList();

                return new ApiResponse<List<SalesDto>>
                {
                    Data = salesDto,
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
                _logger.LogError(ex, "Error retrieving sales data");
                throw;
            }
        }

        public async Task<SalesDto?> GetByIdAsync(int id)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);
                if (sale == null) return null;

                return new SalesDto
                {
                    Id = sale.Id,
                    Source = sale.Source,
                    Revenue = sale.RevenueFormatted,
                    Value = sale.Value
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sale with ID {SaleId}", id);
                throw;
            }
        }

        public async Task<SalesDto> CreateAsync(SalesDto salesDto)
        {
            try
            {
                var sale = new Sales
                {
                    Source = salesDto.Source,
                    RevenueAmount = salesDto.Value,
                    RevenueFormatted = salesDto.Revenue,
                    Value = salesDto.Value,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                salesDto.Id = sale.Id;
                return salesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new sale");
                throw;
            }
        }

        public async Task<SalesDto?> UpdateAsync(int id, SalesDto salesDto)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);
                if (sale == null) return null;

                sale.Source = salesDto.Source;
                sale.RevenueFormatted = salesDto.Revenue;
                sale.Value = salesDto.Value;
                sale.RevenueAmount = salesDto.Value;

                await _context.SaveChangesAsync();

                return salesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale with ID {SaleId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);
                if (sale == null) return false;

                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale with ID {SaleId}", id);
                throw;
            }
        }
    }
}