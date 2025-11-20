using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdSocialMediaStatsService : IAntdSocialMediaStatsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdSocialMediaStatsService> _logger;

        public AntdSocialMediaStatsService(ApplicationDbContext context, ILogger<AntdSocialMediaStatsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdSocialMediaStatsListResponse> GetAllAsync(AntdSocialMediaStatsQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdSocialMediaStats.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Title))
                    query = query.Where(s => s.Title.ToLower() == queryParams.Title.ToLower());

                query = queryParams.SortBy.ToLower() switch
                {
                    "followers" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Followers) : query.OrderBy(s => s.Followers),
                    "following" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Following) : query.OrderBy(s => s.Following),
                    "posts" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Posts) : query.OrderBy(s => s.Posts),
                    "likes" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Likes) : query.OrderBy(s => s.Likes),
                    "comments" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Comments) : query.OrderBy(s => s.Comments),
                    "engagementrate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.EngagementRate) : query.OrderBy(s => s.EngagementRate),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(s => s.Followers) : query.OrderBy(s => s.Followers)
                };

                var total = await query.CountAsync();
                var stats = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdSocialMediaStatsListResponse
                {
                    Success = true,
                    Data = stats.Select(MapToDto).ToList(),
                    Message = "Social media stats retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd social media stats");
                throw;
            }
        }

        public async Task<AntdSocialMediaStatsResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSocialMediaStatsResponse { Success = false, Message = "Invalid social media stats ID format" };

                var stats = await _context.AntdSocialMediaStats.FindAsync(guidId);
                if (stats == null)
                    return new AntdSocialMediaStatsResponse { Success = false, Message = "Social media stats not found" };

                return new AntdSocialMediaStatsResponse { Success = true, Data = MapToDto(stats), Message = "Social media stats retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd social media stats {StatsId}", id);
                throw;
            }
        }

        public async Task<AntdSocialMediaStatsCreateResponse> CreateAsync(AntdSocialMediaStatsDto dto)
        {
            try
            {
                var stats = new AntdSocialMediaStats
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Followers = dto.Followers,
                    Following = dto.Following,
                    Posts = dto.Posts,
                    Likes = dto.Likes,
                    Comments = dto.Comments,
                    EngagementRate = dto.EngagementRate,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdSocialMediaStats.Add(stats);
                await _context.SaveChangesAsync();

                return new AntdSocialMediaStatsCreateResponse { Success = true, Data = MapToDto(stats), Message = "Social media stats created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd social media stats");
                throw;
            }
        }

        public async Task<AntdSocialMediaStatsUpdateResponse> UpdateAsync(string id, AntdSocialMediaStatsDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSocialMediaStatsUpdateResponse { Success = false, Message = "Invalid social media stats ID format" };

                var stats = await _context.AntdSocialMediaStats.FindAsync(guidId);
                if (stats == null)
                    return new AntdSocialMediaStatsUpdateResponse { Success = false, Message = "Social media stats not found" };

                stats.Title = dto.Title;
                stats.Followers = dto.Followers;
                stats.Following = dto.Following;
                stats.Posts = dto.Posts;
                stats.Likes = dto.Likes;
                stats.Comments = dto.Comments;
                stats.EngagementRate = dto.EngagementRate;
                stats.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdSocialMediaStatsUpdateResponse { Success = true, Data = MapToDto(stats), Message = "Social media stats updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd social media stats {StatsId}", id);
                throw;
            }
        }

        public async Task<AntdSocialMediaStatsDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSocialMediaStatsDeleteResponse { Success = false, Message = "Invalid social media stats ID format" };

                var stats = await _context.AntdSocialMediaStats.FindAsync(guidId);
                if (stats == null)
                    return new AntdSocialMediaStatsDeleteResponse { Success = false, Message = "Social media stats not found" };

                _context.AntdSocialMediaStats.Remove(stats);
                await _context.SaveChangesAsync();

                return new AntdSocialMediaStatsDeleteResponse { Success = true, Message = "Social media stats deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd social media stats {StatsId}", id);
                throw;
            }
        }

        private static AntdSocialMediaStatsDto MapToDto(AntdSocialMediaStats stats)
        {
            return new AntdSocialMediaStatsDto
            {
                Id = stats.Id.ToString(),
                Title = stats.Title,
                Followers = stats.Followers,
                Following = stats.Following,
                Posts = stats.Posts,
                Likes = stats.Likes,
                Comments = stats.Comments,
                EngagementRate = stats.EngagementRate
            };
        }
    }
}
