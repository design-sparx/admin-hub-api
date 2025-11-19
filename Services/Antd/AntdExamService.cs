using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdExamService : IAntdExamService
    {
        private readonly ApplicationDbContext _context;

        public AntdExamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AntdExamListResponse> GetAllAsync(AntdExamQueryParams queryParams)
        {
            var query = _context.AntdExams.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(queryParams.Course))
                query = query.Where(e => e.Course.ToLower().Contains(queryParams.Course.ToLower()));

            if (!string.IsNullOrEmpty(queryParams.CourseCode))
                query = query.Where(e => e.CourseCode.ToLower().Contains(queryParams.CourseCode.ToLower()));

            if (queryParams.StudentId.HasValue)
                query = query.Where(e => e.StudentId == queryParams.StudentId.Value);

            if (queryParams.StartDate.HasValue)
                query = query.Where(e => e.ExamDate >= queryParams.StartDate.Value);

            if (queryParams.EndDate.HasValue)
                query = query.Where(e => e.ExamDate <= queryParams.EndDate.Value);

            if (queryParams.MinScore.HasValue)
                query = query.Where(e => e.ExamScore >= queryParams.MinScore.Value);

            if (queryParams.MaxScore.HasValue)
                query = query.Where(e => e.ExamScore <= queryParams.MaxScore.Value);

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply sorting
            query = queryParams.SortBy?.ToLower() switch
            {
                "fullname" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.FullName) : query.OrderBy(e => e.FullName),
                "course" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.Course) : query.OrderBy(e => e.Course),
                "examdate" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.ExamDate) : query.OrderBy(e => e.ExamDate),
                "examscore" => queryParams.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(e => e.ExamScore) : query.OrderBy(e => e.ExamScore),
                _ => query.OrderByDescending(e => e.ExamDate)
            };

            // Apply pagination
            var exams = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            return new AntdExamListResponse
            {
                Data = exams.Select(MapToResponseDto).ToList(),
                Meta = new PaginationMeta
                {
                    CurrentPage = queryParams.Page,
                    PageSize = queryParams.PageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
                }
            };
        }

        public async Task<AntdExamResponse> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid exam ID format");

            var exam = await _context.AntdExams.FindAsync(guid);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {id} not found");

            return new AntdExamResponse { Data = MapToResponseDto(exam) };
        }

        public async Task<AntdExamCreateResponse> CreateAsync(AntdExamDto dto)
        {
            var exam = new AntdExam
            {
                StudentId = dto.StudentId,
                FullName = dto.FullName,
                Email = dto.Email,
                Course = dto.Course,
                CourseCode = dto.CourseCode,
                ExamDate = dto.ExamDate,
                ExamTime = dto.ExamTime,
                ExamDuration = dto.ExamDuration,
                ExamScore = dto.ExamScore
            };

            _context.AntdExams.Add(exam);
            await _context.SaveChangesAsync();

            return new AntdExamCreateResponse
            {
                Message = "Exam created successfully",
                Data = MapToResponseDto(exam)
            };
        }

        public async Task<AntdExamUpdateResponse> UpdateAsync(string id, AntdExamDto dto)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid exam ID format");

            var exam = await _context.AntdExams.FindAsync(guid);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {id} not found");

            exam.StudentId = dto.StudentId;
            exam.FullName = dto.FullName;
            exam.Email = dto.Email;
            exam.Course = dto.Course;
            exam.CourseCode = dto.CourseCode;
            exam.ExamDate = dto.ExamDate;
            exam.ExamTime = dto.ExamTime;
            exam.ExamDuration = dto.ExamDuration;
            exam.ExamScore = dto.ExamScore;
            exam.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new AntdExamUpdateResponse
            {
                Message = "Exam updated successfully",
                Data = MapToResponseDto(exam)
            };
        }

        public async Task<AntdExamDeleteResponse> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new KeyNotFoundException("Invalid exam ID format");

            var exam = await _context.AntdExams.FindAsync(guid);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {id} not found");

            _context.AntdExams.Remove(exam);
            await _context.SaveChangesAsync();

            return new AntdExamDeleteResponse { Message = "Exam deleted successfully" };
        }

        private static AntdExamResponseDto MapToResponseDto(AntdExam exam)
        {
            return new AntdExamResponseDto
            {
                Id = exam.Id,
                StudentId = exam.StudentId,
                FullName = exam.FullName,
                Email = exam.Email,
                Course = exam.Course,
                CourseCode = exam.CourseCode,
                ExamDate = exam.ExamDate,
                ExamTime = exam.ExamTime,
                ExamDuration = exam.ExamDuration,
                ExamScore = exam.ExamScore,
                CreatedAt = exam.CreatedAt,
                UpdatedAt = exam.UpdatedAt
            };
        }
    }
}
