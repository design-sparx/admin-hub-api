using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdCourseService : IAntdCourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdCourseService> _logger;

        public AntdCourseService(ApplicationDbContext context, ILogger<AntdCourseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdCourseListResponse> GetAllAsync(AntdCourseQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdCourses.AsQueryable();

                if (!string.IsNullOrEmpty(queryParams.Department))
                    query = query.Where(c => c.Department.ToLower() == queryParams.Department.ToLower());

                if (!string.IsNullOrEmpty(queryParams.InstructorName))
                    query = query.Where(c => c.InstructorName.Contains(queryParams.InstructorName));

                if (queryParams.MinCreditHours.HasValue)
                    query = query.Where(c => c.CreditHours >= queryParams.MinCreditHours.Value);

                if (queryParams.MaxCreditHours.HasValue)
                    query = query.Where(c => c.CreditHours <= queryParams.MaxCreditHours.Value);

                if (queryParams.StartDateFrom.HasValue)
                    query = query.Where(c => c.StartDate >= queryParams.StartDateFrom.Value);

                if (queryParams.StartDateTo.HasValue)
                    query = query.Where(c => c.StartDate <= queryParams.StartDateTo.Value);

                query = queryParams.SortBy.ToLower() switch
                {
                    "startdate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.StartDate) : query.OrderBy(c => c.StartDate),
                    "enddate" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.EndDate) : query.OrderBy(c => c.EndDate),
                    "credithours" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.CreditHours) : query.OrderBy(c => c.CreditHours),
                    "name" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                    "totallessons" => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.TotalLessons) : query.OrderBy(c => c.TotalLessons),
                    _ => queryParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(c => c.StartDate) : query.OrderBy(c => c.StartDate)
                };

                var total = await query.CountAsync();
                var courses = await query.Skip((queryParams.Page - 1) * queryParams.Limit).Take(queryParams.Limit).ToListAsync();

                return new AntdCourseListResponse
                {
                    Succeeded = true,
                    Data = courses.Select(MapToDto).ToList(),
                    Message = "Courses retrieved successfully",
                    Meta = new PaginationMeta { Page = queryParams.Page, Limit = queryParams.Limit, Total = total, TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit) }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd courses");
                throw;
            }
        }

        public async Task<AntdCourseResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdCourseResponse { Succeeded = false, Message = "Invalid course ID format" };

                var course = await _context.AntdCourses.FindAsync(guidId);
                if (course == null)
                    return new AntdCourseResponse { Succeeded = false, Message = "Course not found" };

                return new AntdCourseResponse { Succeeded = true, Data = MapToDto(course), Message = "Course retrieved successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd course {CourseId}", id);
                throw;
            }
        }

        public async Task<AntdCourseCreateResponse> CreateAsync(AntdCourseDto dto)
        {
            try
            {
                var course = new AntdCourse
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Code = dto.Code,
                    Description = dto.Description,
                    InstructorName = dto.InstructorName,
                    StartDate = DateTime.Parse(dto.StartDate).ToUniversalTime(),
                    EndDate = DateTime.Parse(dto.EndDate).ToUniversalTime(),
                    CreditHours = dto.CreditHours,
                    Department = dto.Department,
                    Prerequisites = dto.Prerequisites,
                    CourseLocation = dto.CourseLocation,
                    TotalLessons = dto.TotalLessons,
                    CurrentLessons = dto.CurrentLessons,
                    FavoriteColor = dto.FavoriteColor,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdCourses.Add(course);
                await _context.SaveChangesAsync();

                return new AntdCourseCreateResponse { Succeeded = true, Data = MapToDto(course), Message = "Course created successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd course");
                throw;
            }
        }

        public async Task<AntdCourseUpdateResponse> UpdateAsync(string id, AntdCourseDto dto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdCourseUpdateResponse { Succeeded = false, Message = "Invalid course ID format" };

                var course = await _context.AntdCourses.FindAsync(guidId);
                if (course == null)
                    return new AntdCourseUpdateResponse { Succeeded = false, Message = "Course not found" };

                course.Name = dto.Name;
                course.Code = dto.Code;
                course.Description = dto.Description;
                course.InstructorName = dto.InstructorName;
                course.StartDate = DateTime.Parse(dto.StartDate).ToUniversalTime();
                course.EndDate = DateTime.Parse(dto.EndDate).ToUniversalTime();
                course.CreditHours = dto.CreditHours;
                course.Department = dto.Department;
                course.Prerequisites = dto.Prerequisites;
                course.CourseLocation = dto.CourseLocation;
                course.TotalLessons = dto.TotalLessons;
                course.CurrentLessons = dto.CurrentLessons;
                course.FavoriteColor = dto.FavoriteColor;
                course.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdCourseUpdateResponse { Succeeded = true, Data = MapToDto(course), Message = "Course updated successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd course {CourseId}", id);
                throw;
            }
        }

        public async Task<AntdCourseDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdCourseDeleteResponse { Succeeded = false, Message = "Invalid course ID format" };

                var course = await _context.AntdCourses.FindAsync(guidId);
                if (course == null)
                    return new AntdCourseDeleteResponse { Succeeded = false, Message = "Course not found" };

                _context.AntdCourses.Remove(course);
                await _context.SaveChangesAsync();

                return new AntdCourseDeleteResponse { Succeeded = true, Message = "Course deleted successfully" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd course {CourseId}", id);
                throw;
            }
        }

        private static AntdCourseDto MapToDto(AntdCourse course)
        {
            return new AntdCourseDto
            {
                Id = course.Id.ToString(),
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                InstructorName = course.InstructorName,
                StartDate = course.StartDate.ToString("M/d/yyyy"),
                EndDate = course.EndDate.ToString("M/d/yyyy"),
                CreditHours = course.CreditHours,
                Department = course.Department,
                Prerequisites = course.Prerequisites,
                CourseLocation = course.CourseLocation,
                TotalLessons = course.TotalLessons,
                CurrentLessons = course.CurrentLessons,
                FavoriteColor = course.FavoriteColor
            };
        }
    }
}
