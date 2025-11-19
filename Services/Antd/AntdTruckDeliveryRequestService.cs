using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdTruckDeliveryRequestService : IAntdTruckDeliveryRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdTruckDeliveryRequestService> _logger;

        public AntdTruckDeliveryRequestService(ApplicationDbContext context, ILogger<AntdTruckDeliveryRequestService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdTruckDeliveryRequestListResponse> GetAllAsync(AntdTruckDeliveryRequestQueryParams queryParams)
        {
            var query = _context.AntdTruckDeliveryRequests.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Name))
                query = query.Where(x => x.Name.Contains(queryParams.Name));

            if (!string.IsNullOrEmpty(queryParams.TruckType))
                query = query.Where(x => x.TruckType == queryParams.TruckType);

            if (!string.IsNullOrEmpty(queryParams.DeliveryStatus))
                query = query.Where(x => x.DeliveryStatus == queryParams.DeliveryStatus);

            if (!string.IsNullOrEmpty(queryParams.DriverName))
                query = query.Where(x => x.DriverName.Contains(queryParams.DriverName));

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "name" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),
                "deliverydate" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DeliveryDate)
                    : query.OrderBy(x => x.DeliveryDate),
                "trucktype" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.TruckType)
                    : query.OrderBy(x => x.TruckType),
                "cargoweight" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.CargoWeight)
                    : query.OrderBy(x => x.CargoWeight),
                "deliverystatus" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DeliveryStatus)
                    : query.OrderBy(x => x.DeliveryStatus),
                "drivername" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.DriverName)
                    : query.OrderBy(x => x.DriverName),
                _ => query.OrderByDescending(x => x.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(x => new AntdTruckDeliveryRequestDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PickupLocation = x.PickupLocation,
                    DeliveryLocation = x.DeliveryLocation,
                    DeliveryDate = x.DeliveryDate,
                    DeliveryTime = x.DeliveryTime,
                    TruckType = x.TruckType,
                    CargoWeight = x.CargoWeight,
                    DeliveryStatus = x.DeliveryStatus,
                    DriverName = x.DriverName,
                    ContactNumber = x.ContactNumber
                })
                .ToListAsync();

            return new AntdTruckDeliveryRequestListResponse
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

        public async Task<AntdTruckDeliveryRequestDto> GetByIdAsync(int id)
        {
            var entity = await _context.AntdTruckDeliveryRequests.FindAsync(id);
            if (entity == null) return null;

            return new AntdTruckDeliveryRequestDto
            {
                Id = entity.Id,
                Name = entity.Name,
                PickupLocation = entity.PickupLocation,
                DeliveryLocation = entity.DeliveryLocation,
                DeliveryDate = entity.DeliveryDate,
                DeliveryTime = entity.DeliveryTime,
                TruckType = entity.TruckType,
                CargoWeight = entity.CargoWeight,
                DeliveryStatus = entity.DeliveryStatus,
                DriverName = entity.DriverName,
                ContactNumber = entity.ContactNumber
            };
        }

        public async Task<AntdTruckDeliveryRequestDto> CreateAsync(AntdTruckDeliveryRequestCreateDto createDto)
        {
            var entity = new AntdTruckDeliveryRequest
            {
                Name = createDto.Name,
                PickupLocation = createDto.PickupLocation,
                DeliveryLocation = createDto.DeliveryLocation,
                DeliveryDate = createDto.DeliveryDate,
                DeliveryTime = createDto.DeliveryTime,
                TruckType = createDto.TruckType,
                CargoWeight = createDto.CargoWeight,
                DeliveryStatus = createDto.DeliveryStatus,
                DriverName = createDto.DriverName,
                ContactNumber = createDto.ContactNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.AntdTruckDeliveryRequests.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<AntdTruckDeliveryRequestDto> UpdateAsync(int id, AntdTruckDeliveryRequestUpdateDto updateDto)
        {
            var entity = await _context.AntdTruckDeliveryRequests.FindAsync(id);
            if (entity == null) return null;

            if (updateDto.Name != null) entity.Name = updateDto.Name;
            if (updateDto.PickupLocation != null) entity.PickupLocation = updateDto.PickupLocation;
            if (updateDto.DeliveryLocation != null) entity.DeliveryLocation = updateDto.DeliveryLocation;
            if (updateDto.DeliveryDate.HasValue) entity.DeliveryDate = updateDto.DeliveryDate.Value;
            if (updateDto.DeliveryTime.HasValue) entity.DeliveryTime = updateDto.DeliveryTime.Value;
            if (updateDto.TruckType != null) entity.TruckType = updateDto.TruckType;
            if (updateDto.CargoWeight.HasValue) entity.CargoWeight = updateDto.CargoWeight.Value;
            if (updateDto.DeliveryStatus != null) entity.DeliveryStatus = updateDto.DeliveryStatus;
            if (updateDto.DriverName != null) entity.DriverName = updateDto.DriverName;
            if (updateDto.ContactNumber != null) entity.ContactNumber = updateDto.ContactNumber;

            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AntdTruckDeliveryRequests.FindAsync(id);
            if (entity == null) return false;

            _context.AntdTruckDeliveryRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
