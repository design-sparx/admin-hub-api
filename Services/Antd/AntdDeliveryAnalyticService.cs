using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdDeliveryAnalyticService : IAntdDeliveryAnalyticService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdDeliveryAnalyticService> _logger;

        public AntdDeliveryAnalyticService(ApplicationDbContext context, ILogger<AntdDeliveryAnalyticService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdDeliveryAnalyticListResponse> GetAllAsync(AntdDeliveryAnalyticQueryParams queryParams)
        {
            var query = _context.AntdDeliveryAnalytics.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Month))
                query = query.Where(x => x.Month == queryParams.Month);

            if (!string.IsNullOrEmpty(queryParams.Status))
                query = query.Where(x => x.Status == queryParams.Status);

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "value" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Value)
                    : query.OrderBy(x => x.Value),
                "month" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Month)
                    : query.OrderBy(x => x.Month),
                "status" => queryParams.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),
                _ => query.OrderByDescending(x => x.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(x => new AntdDeliveryAnalyticDto
                {
                    Id = x.Id,
                    Value = x.Value,
                    Month = x.Month,
                    Status = x.Status
                })
                .ToListAsync();

            return new AntdDeliveryAnalyticListResponse
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

        public async Task<AntdDeliveryAnalyticDto> GetByIdAsync(int id)
        {
            var entity = await _context.AntdDeliveryAnalytics.FindAsync(id);
            if (entity == null) return null;

            return new AntdDeliveryAnalyticDto
            {
                Id = entity.Id,
                Value = entity.Value,
                Month = entity.Month,
                Status = entity.Status
            };
        }

        public async Task<AntdDeliveryAnalyticDto> CreateAsync(AntdDeliveryAnalyticCreateDto createDto)
        {
            var entity = new AntdDeliveryAnalytic
            {
                Value = createDto.Value,
                Month = createDto.Month,
                Status = createDto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.AntdDeliveryAnalytics.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<AntdDeliveryAnalyticDto> UpdateAsync(int id, AntdDeliveryAnalyticUpdateDto updateDto)
        {
            var entity = await _context.AntdDeliveryAnalytics.FindAsync(id);
            if (entity == null) return null;

            if (updateDto.Value.HasValue) entity.Value = updateDto.Value.Value;
            if (updateDto.Month != null) entity.Month = updateDto.Month;
            if (updateDto.Status != null) entity.Status = updateDto.Status;

            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AntdDeliveryAnalytics.FindAsync(id);
            if (entity == null) return false;

            _context.AntdDeliveryAnalytics.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
