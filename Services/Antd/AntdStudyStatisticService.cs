using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdStudyStatisticService : IAntdStudyStatisticService
    {
        private readonly ApplicationDbContext _context;

        public AntdStudyStatisticService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AntdStudyStatisticListResponse> GetAllAsync(AntdStudyStatisticQueryParams queryParams)
        {
            var query = _context.AntdStudyStatistics.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Category))
                query = query.Where(s => s.Category.ToLower().Contains(queryParams.Category.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Month))
                query = query.Where(s => s.Month.ToLower().Contains(queryParams.Month.ToLower()));

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "value" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.Value) : query.OrderBy(s => s.Value),
                "category" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.Category) : query.OrderBy(s => s.Category),
                "month" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(s => s.Month) : query.OrderBy(s => s.Month),
                _ => query.OrderByDescending(s => s.CreatedAt)
            };

            // Apply pagination
            var statistics = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new AntdStudyStatisticListResponse
            {
                Data = statistics.Select(MapToResponseDto).ToList(),
                Meta = new PaginationMeta
                {
                    Page = queryParams.Page,
                    Limit = queryParams.PageSize,
                    Total = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
                }
            };
        }

        public async Task<AntdStudyStatisticResponse> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid statistic ID format");

            var statistic = await _context.AntdStudyStatistics.FindAsync(guid);
            if (statistic == null)
                throw new KeyNotFoundException($"Study statistic with ID {id} not found");

            return new AntdStudyStatisticResponse { Data = MapToResponseDto(statistic) };
        }

        public async Task<AntdStudyStatisticCreateResponse> CreateAsync(AntdStudyStatisticDto dto)
        {
            var statistic = new AntdStudyStatistic
            {
                Value = dto.Value,
                Category = dto.Category,
                Month = dto.Month
            };

            _context.AntdStudyStatistics.Add(statistic);
            await _context.SaveChangesAsync();

            return new AntdStudyStatisticCreateResponse
            {
                Message = "Study statistic created successfully",
                Data = MapToResponseDto(statistic)
            };
        }

        public async Task<AntdStudyStatisticUpdateResponse> UpdateAsync(string id, AntdStudyStatisticDto dto)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid statistic ID format");

            var statistic = await _context.AntdStudyStatistics.FindAsync(guid);
            if (statistic == null)
                throw new KeyNotFoundException($"Study statistic with ID {id} not found");

            statistic.Value = dto.Value;
            statistic.Category = dto.Category;
            statistic.Month = dto.Month;
            statistic.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new AntdStudyStatisticUpdateResponse
            {
                Message = "Study statistic updated successfully",
                Data = MapToResponseDto(statistic)
            };
        }

        public async Task<AntdStudyStatisticDeleteResponse> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid statistic ID format");

            var statistic = await _context.AntdStudyStatistics.FindAsync(guid);
            if (statistic == null)
                throw new KeyNotFoundException($"Study statistic with ID {id} not found");

            _context.AntdStudyStatistics.Remove(statistic);
            await _context.SaveChangesAsync();

            return new AntdStudyStatisticDeleteResponse { Message = "Study statistic deleted successfully" };
        }

        private static AntdStudyStatisticResponseDto MapToResponseDto(AntdStudyStatistic statistic)
        {
            return new AntdStudyStatisticResponseDto
            {
                Id = statistic.Id,
                Value = statistic.Value,
                Category = statistic.Category,
                Month = statistic.Month,
                CreatedAt = statistic.CreatedAt,
                UpdatedAt = statistic.UpdatedAt
            };
        }
    }
}
