using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdTruckService : IAntdTruckService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdTruckService> _logger;

        public AntdTruckService(ApplicationDbContext context, ILogger<AntdTruckService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdTruckListResponse> GetAllAsync(AntdTruckQueryParams queryParams)
        {
            var query = _context.AntdTrucks.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Make))
                query = query.Where(x => x.Make.Contains(queryParams.Make));

            if (!string.IsNullOrEmpty(queryParams.Model))
                query = query.Where(x => x.Model.Contains(queryParams.Model));

            if (!string.IsNullOrEmpty(queryParams.Status))
                query = query.Where(x => x.Status == queryParams.Status);

            if (queryParams.Availability.HasValue)
                query = query.Where(x => x.Availability == queryParams.Availability.Value);

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "make" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Make)
                    : query.OrderBy(x => x.Make),
                "model" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Model)
                    : query.OrderBy(x => x.Model),
                "year" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Year)
                    : query.OrderBy(x => x.Year),
                "price" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Price)
                    : query.OrderBy(x => x.Price),
                "mileage" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Mileage)
                    : query.OrderBy(x => x.Mileage),
                "status" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),
                _ => query.OrderByDescending(x => x.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(x => new AntdTruckDto
                {
                    Id = x.Id,
                    TruckId = x.TruckId,
                    Make = x.Make,
                    Model = x.Model,
                    Year = x.Year,
                    Mileage = x.Mileage,
                    Price = x.Price,
                    Color = x.Color,
                    Status = x.Status,
                    Availability = x.Availability,
                    Origin = x.Origin,
                    Destination = x.Destination,
                    Progress = x.Progress
                })
                .ToListAsync();

            return new AntdTruckListResponse
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

        public async Task<AntdTruckDto> GetByIdAsync(int id)
        {
            var entity = await _context.AntdTrucks.FindAsync(id);
            if (entity == null) return null;

            return new AntdTruckDto
            {
                Id = entity.Id,
                TruckId = entity.TruckId,
                Make = entity.Make,
                Model = entity.Model,
                Year = entity.Year,
                Mileage = entity.Mileage,
                Price = entity.Price,
                Color = entity.Color,
                Status = entity.Status,
                Availability = entity.Availability,
                Origin = entity.Origin,
                Destination = entity.Destination,
                Progress = entity.Progress
            };
        }

        public async Task<AntdTruckDto> CreateAsync(AntdTruckCreateDto createDto)
        {
            var entity = new AntdTruck
            {
                TruckId = createDto.TruckId,
                Make = createDto.Make,
                Model = createDto.Model,
                Year = createDto.Year,
                Mileage = createDto.Mileage,
                Price = createDto.Price,
                Color = createDto.Color,
                Status = createDto.Status,
                Availability = createDto.Availability,
                Origin = createDto.Origin,
                Destination = createDto.Destination,
                Progress = createDto.Progress,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.AntdTrucks.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<AntdTruckDto> UpdateAsync(int id, AntdTruckUpdateDto updateDto)
        {
            var entity = await _context.AntdTrucks.FindAsync(id);
            if (entity == null) return null;

            if (updateDto.TruckId.HasValue) entity.TruckId = updateDto.TruckId.Value;
            if (updateDto.Make != null) entity.Make = updateDto.Make;
            if (updateDto.Model != null) entity.Model = updateDto.Model;
            if (updateDto.Year.HasValue) entity.Year = updateDto.Year.Value;
            if (updateDto.Mileage.HasValue) entity.Mileage = updateDto.Mileage.Value;
            if (updateDto.Price.HasValue) entity.Price = updateDto.Price.Value;
            if (updateDto.Color != null) entity.Color = updateDto.Color;
            if (updateDto.Status != null) entity.Status = updateDto.Status;
            if (updateDto.Availability.HasValue) entity.Availability = updateDto.Availability.Value;
            if (updateDto.Origin != null) entity.Origin = updateDto.Origin;
            if (updateDto.Destination != null) entity.Destination = updateDto.Destination;
            if (updateDto.Progress.HasValue) entity.Progress = updateDto.Progress.Value;

            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AntdTrucks.FindAsync(id);
            if (entity == null) return false;

            _context.AntdTrucks.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
