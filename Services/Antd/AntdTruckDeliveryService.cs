using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdTruckDeliveryService : IAntdTruckDeliveryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdTruckDeliveryService> _logger;

        public AntdTruckDeliveryService(ApplicationDbContext context, ILogger<AntdTruckDeliveryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdTruckDeliveryListResponse> GetAllAsync(AntdTruckDeliveryQueryParams queryParams)
        {
            var query = _context.AntdTruckDeliveries.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.CustomerName))
                query = query.Where(x => x.CustomerName.Contains(queryParams.CustomerName));

            if (!string.IsNullOrEmpty(queryParams.DriverName))
                query = query.Where(x => x.DriverName.Contains(queryParams.DriverName));

            if (!string.IsNullOrEmpty(queryParams.OriginCity))
                query = query.Where(x => x.OriginCity.Contains(queryParams.OriginCity));

            if (!string.IsNullOrEmpty(queryParams.DestinationCity))
                query = query.Where(x => x.DestinationCity.Contains(queryParams.DestinationCity));

            if (!string.IsNullOrEmpty(queryParams.DeliveryStatus))
                query = query.Where(x => x.DeliveryStatus == queryParams.DeliveryStatus);

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "customername" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.CustomerName)
                    : query.OrderBy(x => x.CustomerName),
                "drivername" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DriverName)
                    : query.OrderBy(x => x.DriverName),
                "shipmentdate" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.ShipmentDate)
                    : query.OrderBy(x => x.ShipmentDate),
                "shipmentcost" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.ShipmentCost)
                    : query.OrderBy(x => x.ShipmentCost),
                "deliverystatus" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DeliveryStatus)
                    : query.OrderBy(x => x.DeliveryStatus),
                _ => query.OrderByDescending(x => x.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(x => new AntdTruckDeliveryDto
                {
                    Id = x.Id,
                    ShipmentId = x.ShipmentId,
                    TruckId = x.TruckId,
                    CustomerId = x.CustomerId,
                    CustomerName = x.CustomerName,
                    DriverName = x.DriverName,
                    OriginCity = x.OriginCity,
                    DestinationCity = x.DestinationCity,
                    ShipmentDate = x.ShipmentDate,
                    DeliveryTime = x.DeliveryTime,
                    ShipmentWeight = x.ShipmentWeight,
                    DeliveryStatus = x.DeliveryStatus,
                    ShipmentCost = x.ShipmentCost,
                    FavoriteColor = x.FavoriteColor
                })
                .ToListAsync();

            return new AntdTruckDeliveryListResponse
            {
                Data = items,
                Meta = new PaginationMeta
                {
                    Page = queryParams.Page,
                    Limit = queryParams.PageSize,
                    Total = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
                }
            };
        }

        public async Task<AntdTruckDeliveryDto> GetByIdAsync(int id)
        {
            var entity = await _context.AntdTruckDeliveries.FindAsync(id);
            if (entity == null) return null;

            return new AntdTruckDeliveryDto
            {
                Id = entity.Id,
                ShipmentId = entity.ShipmentId,
                TruckId = entity.TruckId,
                CustomerId = entity.CustomerId,
                CustomerName = entity.CustomerName,
                DriverName = entity.DriverName,
                OriginCity = entity.OriginCity,
                DestinationCity = entity.DestinationCity,
                ShipmentDate = entity.ShipmentDate,
                DeliveryTime = entity.DeliveryTime,
                ShipmentWeight = entity.ShipmentWeight,
                DeliveryStatus = entity.DeliveryStatus,
                ShipmentCost = entity.ShipmentCost,
                FavoriteColor = entity.FavoriteColor
            };
        }

        public async Task<AntdTruckDeliveryDto> CreateAsync(AntdTruckDeliveryCreateDto createDto)
        {
            var entity = new AntdTruckDelivery
            {
                ShipmentId = createDto.ShipmentId,
                TruckId = createDto.TruckId,
                CustomerId = createDto.CustomerId,
                CustomerName = createDto.CustomerName,
                DriverName = createDto.DriverName,
                OriginCity = createDto.OriginCity,
                DestinationCity = createDto.DestinationCity,
                ShipmentDate = createDto.ShipmentDate,
                DeliveryTime = createDto.DeliveryTime,
                ShipmentWeight = createDto.ShipmentWeight,
                DeliveryStatus = createDto.DeliveryStatus,
                ShipmentCost = createDto.ShipmentCost,
                FavoriteColor = createDto.FavoriteColor,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.AntdTruckDeliveries.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<AntdTruckDeliveryDto> UpdateAsync(int id, AntdTruckDeliveryUpdateDto updateDto)
        {
            var entity = await _context.AntdTruckDeliveries.FindAsync(id);
            if (entity == null) return null;

            if (updateDto.ShipmentId.HasValue) entity.ShipmentId = updateDto.ShipmentId.Value;
            if (updateDto.TruckId.HasValue) entity.TruckId = updateDto.TruckId.Value;
            if (updateDto.CustomerId.HasValue) entity.CustomerId = updateDto.CustomerId.Value;
            if (updateDto.CustomerName != null) entity.CustomerName = updateDto.CustomerName;
            if (updateDto.DriverName != null) entity.DriverName = updateDto.DriverName;
            if (updateDto.OriginCity != null) entity.OriginCity = updateDto.OriginCity;
            if (updateDto.DestinationCity != null) entity.DestinationCity = updateDto.DestinationCity;
            if (updateDto.ShipmentDate.HasValue) entity.ShipmentDate = updateDto.ShipmentDate.Value;
            if (updateDto.DeliveryTime.HasValue) entity.DeliveryTime = updateDto.DeliveryTime.Value;
            if (updateDto.ShipmentWeight.HasValue) entity.ShipmentWeight = updateDto.ShipmentWeight.Value;
            if (updateDto.DeliveryStatus != null) entity.DeliveryStatus = updateDto.DeliveryStatus;
            if (updateDto.ShipmentCost.HasValue) entity.ShipmentCost = updateDto.ShipmentCost.Value;
            if (updateDto.FavoriteColor != null) entity.FavoriteColor = updateDto.FavoriteColor;

            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AntdTruckDeliveries.FindAsync(id);
            if (entity == null) return false;

            _context.AntdTruckDeliveries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
