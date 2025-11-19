using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdOrderService : IAntdOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdOrderService> _logger;

        public AntdOrderService(ApplicationDbContext context, ILogger<AntdOrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdOrderListResponse> GetAllAsync(AntdOrderQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdOrders.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Status))
                    query = query.Where(o => o.Status.ToLower() == queryParams.Status.ToLower());

                if (!string.IsNullOrEmpty(queryParams.PaymentMethod))
                    query = query.Where(o => o.PaymentMethod.ToLower() == queryParams.PaymentMethod.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Country))
                    query = query.Where(o => o.Country.ToLower() == queryParams.Country.ToLower());

                if (queryParams.OrderDateFrom.HasValue)
                    query = query.Where(o => o.OrderDate >= queryParams.OrderDateFrom.Value);

                if (queryParams.OrderDateTo.HasValue)
                    query = query.Where(o => o.OrderDate <= queryParams.OrderDateTo.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "price" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(o => o.Price) : query.OrderBy(o => o.Price),
                    "status" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(o => o.Status) : query.OrderBy(o => o.Status),
                    "customername" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(o => o.CustomerName) : query.OrderBy(o => o.CustomerName),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(o => o.OrderDate) : query.OrderBy(o => o.OrderDate)
                };

                var total = await query.CountAsync();
                var orders = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdOrderListResponse
                {
                    Succeeded = true,
                    Data = orders.Select(MapToDto).ToList(),
                    Message = "Orders retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd orders");
                throw;
            }
        }

        public async Task<AntdOrderResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdOrderResponse { Succeeded = false, Message = "Invalid order ID format" };

                var order = await _context.AntdOrders.FindAsync(guidId);
                if (order == null)
                    return new AntdOrderResponse { Succeeded = false, Message = "Order not found" };

                return new AntdOrderResponse { Succeeded = true, Data = MapToDto(order), Message = "Order retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd order {OrderId}", id);
                throw;
            }
        }

        public async Task<AntdOrderListResponse> GetRecentOrdersAsync(int limit)
        {
            try
            {
                var orders = await _context.AntdOrders.OrderByDescending(o => o.OrderDate).Take(limit).ToListAsync();
                return new AntdOrderListResponse { Succeeded = true, Data = orders.Select(MapToDto).ToList(), Message = "Recent orders retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recent Antd orders");
                throw;
            }
        }

        public async Task<AntdOrderCreateResponse> CreateAsync(AntdOrderDto orderDto)
        {
            try
            {
                var order = new AntdOrder
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.Parse(orderDto.CustomerId),
                    ProductId = Guid.Parse(orderDto.ProductId),
                    Quantity = orderDto.Quantity,
                    Price = orderDto.Price,
                    OrderDate = DateTime.Parse(orderDto.OrderDate).ToUniversalTime(),
                    ShippingAddress = orderDto.ShippingAddress,
                    City = orderDto.City,
                    State = orderDto.State ?? string.Empty,
                    PostalCode = orderDto.PostalCode ?? string.Empty,
                    Country = orderDto.Country,
                    PaymentMethod = orderDto.PaymentMethod,
                    Status = orderDto.Status,
                    TrackingNumber = orderDto.TrackingNumber,
                    ShippingCost = orderDto.ShippingCost,
                    Tax = orderDto.Tax,
                    ProductName = orderDto.ProductName,
                    CustomerName = orderDto.CustomerName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdOrders.Add(order);
                await _context.SaveChangesAsync();

                return new AntdOrderCreateResponse { Succeeded = true, Data = MapToDto(order), Message = "Order created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd order");
                throw;
            }
        }

        public async Task<AntdOrderUpdateResponse> UpdateAsync(string id, AntdOrderDto orderDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdOrderUpdateResponse { Succeeded = false, Message = "Invalid order ID format" };

                var order = await _context.AntdOrders.FindAsync(guidId);
                if (order == null)
                    return new AntdOrderUpdateResponse { Succeeded = false, Message = "Order not found" };

                order.CustomerId = Guid.Parse(orderDto.CustomerId);
                order.ProductId = Guid.Parse(orderDto.ProductId);
                order.Quantity = orderDto.Quantity;
                order.Price = orderDto.Price;
                order.OrderDate = DateTime.Parse(orderDto.OrderDate).ToUniversalTime();
                order.ShippingAddress = orderDto.ShippingAddress;
                order.City = orderDto.City;
                order.State = orderDto.State ?? string.Empty;
                order.PostalCode = orderDto.PostalCode ?? string.Empty;
                order.Country = orderDto.Country;
                order.PaymentMethod = orderDto.PaymentMethod;
                order.Status = orderDto.Status;
                order.TrackingNumber = orderDto.TrackingNumber;
                order.ShippingCost = orderDto.ShippingCost;
                order.Tax = orderDto.Tax;
                order.ProductName = orderDto.ProductName;
                order.CustomerName = orderDto.CustomerName;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdOrderUpdateResponse { Succeeded = true, Data = MapToDto(order), Message = "Order updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd order {OrderId}", id);
                throw;
            }
        }

        public async Task<AntdOrderDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdOrderDeleteResponse { Succeeded = false, Message = "Invalid order ID format" };

                var order = await _context.AntdOrders.FindAsync(guidId);
                if (order == null)
                    return new AntdOrderDeleteResponse { Succeeded = false, Message = "Order not found" };

                _context.AntdOrders.Remove(order);
                await _context.SaveChangesAsync();

                return new AntdOrderDeleteResponse { Succeeded = true, Message = "Order deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd order {OrderId}", id);
                throw;
            }
        }

        private static AntdOrderDto MapToDto(AntdOrder order)
        {
            return new AntdOrderDto
            {
                OrderId = order.Id.ToString(),
                CustomerId = order.CustomerId.ToString(),
                ProductId = order.ProductId.ToString(),
                Quantity = order.Quantity,
                Price = order.Price,
                OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
                ShippingAddress = order.ShippingAddress,
                City = order.City,
                State = order.State,
                PostalCode = order.PostalCode,
                Country = order.Country,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status,
                TrackingNumber = order.TrackingNumber,
                ShippingCost = order.ShippingCost,
                Tax = order.Tax,
                ProductName = order.ProductName,
                CustomerName = order.CustomerName
            };
        }
    }
}
