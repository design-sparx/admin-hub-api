using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdScheduledPostService : IAntdScheduledPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdScheduledPostService> _logger;

        public AntdScheduledPostService(ApplicationDbContext context, ILogger<AntdScheduledPostService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdScheduledPostListResponse> GetAllAsync(AntdScheduledPostQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdScheduledPosts.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Platform))
                    query = query.Where(p => p.Platform.ToLower() == queryParams.Platform.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Category))
                    query = query.Where(p => p.Category.ToLower() == queryParams.Category.ToLower());

                if (!string.IsNullOrEmpty(queryParams.Author))
                    query = query.Where(p => p.Author.Contains(queryParams.Author));

                if (queryParams.DateFrom.HasValue)
                    query = query.Where(p => p.ScheduledDate >= queryParams.DateFrom.Value);

                if (queryParams.DateTo.HasValue)
                    query = query.Where(p => p.ScheduledDate <= queryParams.DateTo.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "date" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.ScheduledDate) : query.OrderBy(p => p.ScheduledDate),
                    "likescount" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.LikesCount) : query.OrderBy(p => p.LikesCount),
                    "commentscount" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.CommentsCount) : query.OrderBy(p => p.CommentsCount),
                    "sharescount" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.SharesCount) : query.OrderBy(p => p.SharesCount),
                    "author" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Author) : query.OrderBy(p => p.Author),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.ScheduledDate) : query.OrderBy(p => p.ScheduledDate)
                };

                var total = await query.CountAsync();
                var posts = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdScheduledPostListResponse
                {
                    Success = true,
                    Data = posts.Select(MapToDto).ToList(),
                    Message = "Scheduled posts retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd scheduled posts");
                throw;
            }
        }

        public async Task<AntdScheduledPostResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdScheduledPostResponse { Success = false, Message = "Invalid scheduled post ID format" };

                var post = await _context.AntdScheduledPosts.FindAsync(guidId);
                if (post == null)
                    return new AntdScheduledPostResponse { Success = false, Message = "Scheduled post not found" };

                return new AntdScheduledPostResponse { Success = true, Data = MapToDto(post), Message = "Scheduled post retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd scheduled post {PostId}", id);
                throw;
            }
        }

        public async Task<AntdScheduledPostCreateResponse> CreateAsync(AntdScheduledPostDto dto)
        {
            try
            {
                var post = new AntdScheduledPost
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Content = dto.Content,
                    ScheduledDate = DateTime.Parse(dto.Date).ToUniversalTime(),
                    ScheduledTime = dto.Time,
                    Author = dto.Author,
                    Category = dto.Category,
                    Tags = dto.Tags,
                    LikesCount = dto.LikesCount,
                    CommentsCount = dto.CommentsCount,
                    SharesCount = dto.SharesCount,
                    ImageUrl = dto.ImageUrl,
                    Link = dto.Link,
                    Location = dto.Location,
                    Hashtags = dto.Hashtags,
                    Platform = dto.Platform,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdScheduledPosts.Add(post);
                await _context.SaveChangesAsync();

                return new AntdScheduledPostCreateResponse { Success = true, Data = MapToDto(post), Message = "Scheduled post created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd scheduled post");
                throw;
            }
        }

        public async Task<AntdScheduledPostUpdateResponse> UpdateAsync(string id, AntdScheduledPostDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdScheduledPostUpdateResponse { Success = false, Message = "Invalid scheduled post ID format" };

                var post = await _context.AntdScheduledPosts.FindAsync(guidId);
                if (post == null)
                    return new AntdScheduledPostUpdateResponse { Success = false, Message = "Scheduled post not found" };

                post.Title = dto.Title;
                post.Content = dto.Content;
                post.ScheduledDate = DateTime.Parse(dto.Date).ToUniversalTime();
                post.ScheduledTime = dto.Time;
                post.Author = dto.Author;
                post.Category = dto.Category;
                post.Tags = dto.Tags;
                post.LikesCount = dto.LikesCount;
                post.CommentsCount = dto.CommentsCount;
                post.SharesCount = dto.SharesCount;
                post.ImageUrl = dto.ImageUrl;
                post.Link = dto.Link;
                post.Location = dto.Location;
                post.Hashtags = dto.Hashtags;
                post.Platform = dto.Platform;
                post.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdScheduledPostUpdateResponse { Success = true, Data = MapToDto(post), Message = "Scheduled post updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd scheduled post {PostId}", id);
                throw;
            }
        }

        public async Task<AntdScheduledPostDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdScheduledPostDeleteResponse { Success = false, Message = "Invalid scheduled post ID format" };

                var post = await _context.AntdScheduledPosts.FindAsync(guidId);
                if (post == null)
                    return new AntdScheduledPostDeleteResponse { Success = false, Message = "Scheduled post not found" };

                _context.AntdScheduledPosts.Remove(post);
                await _context.SaveChangesAsync();

                return new AntdScheduledPostDeleteResponse { Success = true, Message = "Scheduled post deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd scheduled post {PostId}", id);
                throw;
            }
        }

        private static AntdScheduledPostDto MapToDto(AntdScheduledPost post)
        {
            return new AntdScheduledPostDto
            {
                Id = post.Id.ToString(),
                Title = post.Title,
                Content = post.Content,
                Date = post.ScheduledDate.ToString("M/d/yyyy"),
                Time = post.ScheduledTime,
                Author = post.Author,
                Category = post.Category,
                Tags = post.Tags,
                LikesCount = post.LikesCount,
                CommentsCount = post.CommentsCount,
                SharesCount = post.SharesCount,
                ImageUrl = post.ImageUrl,
                Link = post.Link,
                Location = post.Location,
                Hashtags = post.Hashtags,
                Platform = post.Platform
            };
        }
    }
}
