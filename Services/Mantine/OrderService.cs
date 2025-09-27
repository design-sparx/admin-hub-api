using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ApplicationDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<OrderDto>>> GetAllAsync(OrderQueryParams queryParams)
        {
            try
            {
                var query = _context.Orders.AsQueryable();

                // Apply filters
                if (queryParams.Status.HasValue)
                {
                    query = query.Where(o => o.Status == queryParams.Status.Value);
                }

                if (queryParams.PaymentMethod.HasValue)
                {
                    query = query.Where(o => o.PaymentMethod == queryParams.PaymentMethod.Value);
                }

                if (queryParams.DateFrom.HasValue)
                {
                    query = query.Where(o => o.Date >= queryParams.DateFrom.Value);
                }

                if (queryParams.DateTo.HasValue)
                {
                    query = query.Where(o => o.Date <= queryParams.DateTo.Value);
                }

                if (queryParams.MinTotal.HasValue)
                {
                    query = query.Where(o => o.Total >= queryParams.MinTotal.Value);
                }

                if (queryParams.MaxTotal.HasValue)
                {
                    query = query.Where(o => o.Total <= queryParams.MaxTotal.Value);
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "product" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.Product)
                        : query.OrderBy(o => o.Product),
                    "total" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.Total)
                        : query.OrderBy(o => o.Total),
                    "status" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.Status)
                        : query.OrderBy(o => o.Status),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(o => o.Date)
                        : query.OrderBy(o => o.Date)
                };

                var total = await query.CountAsync();
                var orders = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var ordersDto = orders.Select(o => new OrderDto
                {
                    Id = o.Id.ToString(),
                    Product = o.Product,
                    Date = o.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    Total = o.Total,
                    Status = o.Status,
                    PaymentMethod = o.PaymentMethod
                }).ToList();

                return new ApiResponse<List<OrderDto>>
                {
                    Data = ordersDto,
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
                _logger.LogError(ex, "Error retrieving orders data");
                throw;
            }
        }

        public async Task<OrderDto?> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var order = await _context.Orders.FindAsync(guidId);
                if (order == null) return null;

                return new OrderDto
                {
                    Id = order.Id.ToString(),
                    Product = order.Product,
                    Date = order.Date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    Total = order.Total,
                    Status = order.Status,
                    PaymentMethod = order.PaymentMethod
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order with ID {OrderId}", id);
                throw;
            }
        }

        public async Task<OrderDto> CreateAsync(OrderDto orderDto)
        {
            try
            {
                var order = new Orders
                {
                    Id = Guid.NewGuid(),
                    Product = orderDto.Product,
                    Date = DateTime.Parse(orderDto.Date),
                    Total = orderDto.Total,
                    Status = orderDto.Status,
                    PaymentMethod = orderDto.PaymentMethod,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                orderDto.Id = order.Id.ToString();
                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new order");
                throw;
            }
        }

        public async Task<OrderDto?> UpdateAsync(string id, OrderDto orderDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var order = await _context.Orders.FindAsync(guidId);
                if (order == null) return null;

                order.Product = orderDto.Product;
                order.Date = DateTime.Parse(orderDto.Date);
                order.Total = orderDto.Total;
                order.Status = orderDto.Status;
                order.PaymentMethod = orderDto.PaymentMethod;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order with ID {OrderId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var order = await _context.Orders.FindAsync(guidId);
                if (order == null) return false;

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order with ID {OrderId}", id);
                throw;
            }
        }
    }
}