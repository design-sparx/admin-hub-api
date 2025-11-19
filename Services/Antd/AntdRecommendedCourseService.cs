using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdRecommendedCourseService : IAntdRecommendedCourseService
    {
        private readonly ApplicationDbContext _context;

        public AntdRecommendedCourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AntdRecommendedCourseListResponse> GetAllAsync(AntdRecommendedCourseQueryParams queryParams)
        {
            var query = _context.AntdRecommendedCourses.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Level))
                query = query.Where(c => c.Level.ToLower().Contains(queryParams.Level.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Category))
                query = query.Where(c => c.Category.ToLower().Contains(queryParams.Category.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.Instructor))
                query = query.Where(c => c.Instructor.ToLower().Contains(queryParams.Instructor.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.CourseLanguage))
                query = query.Where(c => c.CourseLanguage.ToLower().Contains(queryParams.CourseLanguage.ToLower()));

            if (queryParams.MinPrice.HasValue)
                query = query.Where(c => c.Price >= queryParams.MinPrice.Value);

            if (queryParams.MaxPrice.HasValue)
                query = query.Where(c => c.Price <= queryParams.MaxPrice.Value);

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "name" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "price" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(c => c.Price) : query.OrderBy(c => c.Price),
                "level" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(c => c.Level) : query.OrderBy(c => c.Level),
                "duration" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(c => c.Duration) : query.OrderBy(c => c.Duration),
                "startdate" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(c => c.StartDate) : query.OrderBy(c => c.StartDate),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };

            // Apply pagination
            var courses = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new AntdRecommendedCourseListResponse
            {
                Data = courses.Select(MapToResponseDto).ToList(),
                Meta = new PaginationMeta
                {
                    Page = queryParams.Page,
                    Limit = queryParams.PageSize,
                    Total = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
                }
            };
        }

        public async Task<AntdRecommendedCourseResponse> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid course ID format");

            var course = await _context.AntdRecommendedCourses.FindAsync(guid);
            if (course == null)
                throw new KeyNotFoundException($"Recommended course with ID {id} not found");

            return new AntdRecommendedCourseResponse { Data = MapToResponseDto(course) };
        }

        public async Task<AntdRecommendedCourseCreateResponse> CreateAsync(AntdRecommendedCourseDto dto)
        {
            var course = new AntdRecommendedCourse
            {
                Name = dto.Name,
                Description = dto.Description,
                Duration = dto.Duration,
                Level = dto.Level,
                Price = dto.Price,
                Category = dto.Category,
                Instructor = dto.Instructor,
                StartDate = dto.StartDate,
                CourseLanguage = dto.CourseLanguage,
                FavoriteColor = dto.FavoriteColor,
                Lessons = dto.Lessons
            };

            _context.AntdRecommendedCourses.Add(course);
            await _context.SaveChangesAsync();

            return new AntdRecommendedCourseCreateResponse
            {
                Message = "Recommended course created successfully",
                Data = MapToResponseDto(course)
            };
        }

        public async Task<AntdRecommendedCourseUpdateResponse> UpdateAsync(string id, AntdRecommendedCourseDto dto)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid course ID format");

            var course = await _context.AntdRecommendedCourses.FindAsync(guid);
            if (course == null)
                throw new KeyNotFoundException($"Recommended course with ID {id} not found");

            course.Name = dto.Name;
            course.Description = dto.Description;
            course.Duration = dto.Duration;
            course.Level = dto.Level;
            course.Price = dto.Price;
            course.Category = dto.Category;
            course.Instructor = dto.Instructor;
            course.StartDate = dto.StartDate;
            course.CourseLanguage = dto.CourseLanguage;
            course.FavoriteColor = dto.FavoriteColor;
            course.Lessons = dto.Lessons;
            course.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new AntdRecommendedCourseUpdateResponse
            {
                Message = "Recommended course updated successfully",
                Data = MapToResponseDto(course)
            };
        }

        public async Task<AntdRecommendedCourseDeleteResponse> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid course ID format");

            var course = await _context.AntdRecommendedCourses.FindAsync(guid);
            if (course == null)
                throw new KeyNotFoundException($"Recommended course with ID {id} not found");

            _context.AntdRecommendedCourses.Remove(course);
            await _context.SaveChangesAsync();

            return new AntdRecommendedCourseDeleteResponse { Message = "Recommended course deleted successfully" };
        }

        private static AntdRecommendedCourseResponseDto MapToResponseDto(AntdRecommendedCourse course)
        {
            return new AntdRecommendedCourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
                Level = course.Level,
                Price = course.Price,
                Category = course.Category,
                Instructor = course.Instructor,
                StartDate = course.StartDate,
                CourseLanguage = course.CourseLanguage,
                FavoriteColor = course.FavoriteColor,
                Lessons = course.Lessons,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt
            };
        }
    }
}
