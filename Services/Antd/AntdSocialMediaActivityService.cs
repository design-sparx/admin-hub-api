using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdSocialMediaActivityService : IAntdSocialMediaActivityService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdSocialMediaActivityService> _logger;

        public AntdSocialMediaActivityService(ApplicationDbContext context, ILogger<AntdSocialMediaActivityService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdSocialMediaActivityListResponse> GetAllAsync(AntdSocialMediaActivityQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdSocialMediaActivities.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Platform))
                    query = query.Where(a => a.Platform.ToLower() == queryParams.Platform.ToLower());

                if (!string.IsNullOrEmpty(queryParams.ActivityType))
                    query = query.Where(a => a.ActivityType.ToLower() == queryParams.ActivityType.ToLower());

                if (!string.IsNullOrEmpty(queryParams.UserGender))
                    query = query.Where(a => a.UserGender.ToLower() == queryParams.UserGender.ToLower());

                if (queryParams.MinAge.HasValue)
                    query = query.Where(a => a.UserAge >= queryParams.MinAge.Value);

                if (queryParams.MaxAge.HasValue)
                    query = query.Where(a => a.UserAge <= queryParams.MaxAge.Value);

                if (queryParams.TimestampFrom.HasValue)
                    query = query.Where(a => a.Timestamp >= queryParams.TimestampFrom.Value);

                if (queryParams.TimestampTo.HasValue)
                    query = query.Where(a => a.Timestamp <= queryParams.TimestampTo.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "timestamp" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.Timestamp) : query.OrderBy(a => a.Timestamp),
                    "userage" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.UserAge) : query.OrderBy(a => a.UserAge),
                    "userfriendscount" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.UserFriendsCount) : query.OrderBy(a => a.UserFriendsCount),
                    "author" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.Author) : query.OrderBy(a => a.Author),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(a => a.Timestamp) : query.OrderBy(a => a.Timestamp)
                };

                var total = await query.CountAsync();
                var activities = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdSocialMediaActivityListResponse
                {
                    Succeeded = true,
                    Data = activities.Select(MapToDto).ToList(),
                    Message = "Social media activities retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd social media activities");
                throw;
            }
        }

        public async Task<AntdSocialMediaActivityResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSocialMediaActivityResponse { Succeeded = false, Message = "Invalid activity ID format" };

                var activity = await _context.AntdSocialMediaActivities.FindAsync(guidId);
                if (activity == null)
                    return new AntdSocialMediaActivityResponse { Succeeded = false, Message = "Activity not found" };

                return new AntdSocialMediaActivityResponse { Succeeded = true, Data = MapToDto(activity), Message = "Activity retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd social media activity {ActivityId}", id);
                throw;
            }
        }

        public async Task<AntdSocialMediaActivityCreateResponse> CreateAsync(AntdSocialMediaActivityDto dto)
        {
            try
            {
                var activity = new AntdSocialMediaActivity
                {
                    Id = Guid.NewGuid(),
                    Author = dto.Author,
                    UserId = dto.UserId,
                    ActivityType = dto.ActivityType,
                    Timestamp = DateTime.Parse(dto.Timestamp).ToUniversalTime(),
                    PostContent = dto.PostContent,
                    Platform = dto.Platform,
                    UserLocation = dto.UserLocation,
                    UserAge = dto.UserAge,
                    UserGender = dto.UserGender,
                    UserInterests = dto.UserInterests,
                    UserFriendsCount = dto.UserFriendsCount,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdSocialMediaActivities.Add(activity);
                await _context.SaveChangesAsync();

                return new AntdSocialMediaActivityCreateResponse { Succeeded = true, Data = MapToDto(activity), Message = "Activity created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd social media activity");
                throw;
            }
        }

        public async Task<AntdSocialMediaActivityUpdateResponse> UpdateAsync(string id, AntdSocialMediaActivityDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSocialMediaActivityUpdateResponse { Succeeded = false, Message = "Invalid activity ID format" };

                var activity = await _context.AntdSocialMediaActivities.FindAsync(guidId);
                if (activity == null)
                    return new AntdSocialMediaActivityUpdateResponse { Succeeded = false, Message = "Activity not found" };

                activity.Author = dto.Author;
                activity.UserId = dto.UserId;
                activity.ActivityType = dto.ActivityType;
                activity.Timestamp = DateTime.Parse(dto.Timestamp).ToUniversalTime();
                activity.PostContent = dto.PostContent;
                activity.Platform = dto.Platform;
                activity.UserLocation = dto.UserLocation;
                activity.UserAge = dto.UserAge;
                activity.UserGender = dto.UserGender;
                activity.UserInterests = dto.UserInterests;
                activity.UserFriendsCount = dto.UserFriendsCount;
                activity.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdSocialMediaActivityUpdateResponse { Succeeded = true, Data = MapToDto(activity), Message = "Activity updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd social media activity {ActivityId}", id);
                throw;
            }
        }

        public async Task<AntdSocialMediaActivityDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdSocialMediaActivityDeleteResponse { Succeeded = false, Message = "Invalid activity ID format" };

                var activity = await _context.AntdSocialMediaActivities.FindAsync(guidId);
                if (activity == null)
                    return new AntdSocialMediaActivityDeleteResponse { Succeeded = false, Message = "Activity not found" };

                _context.AntdSocialMediaActivities.Remove(activity);
                await _context.SaveChangesAsync();

                return new AntdSocialMediaActivityDeleteResponse { Succeeded = true, Message = "Activity deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd social media activity {ActivityId}", id);
                throw;
            }
        }

        private static AntdSocialMediaActivityDto MapToDto(AntdSocialMediaActivity activity)
        {
            return new AntdSocialMediaActivityDto
            {
                Id = activity.Id.ToString(),
                Author = activity.Author,
                UserId = activity.UserId,
                ActivityType = activity.ActivityType,
                Timestamp = activity.Timestamp.ToString("MM/dd/yyyy"),
                PostContent = activity.PostContent,
                Platform = activity.Platform,
                UserLocation = activity.UserLocation,
                UserAge = activity.UserAge,
                UserGender = activity.UserGender,
                UserInterests = activity.UserInterests,
                UserFriendsCount = activity.UserFriendsCount
            };
        }
    }
}
