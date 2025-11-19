using AdminHubApi.Data;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Enums.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ApplicationDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<TaskDto>>> GetAllAsync(TaskQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdTasks.AsQueryable();

                // Apply filters
                if (queryParams.Status.HasValue)
                    query = query.Where(t => t.Status == queryParams.Status.Value);

                if (queryParams.Priority.HasValue)
                    query = query.Where(t => t.Priority == queryParams.Priority.Value);

                if (queryParams.Category.HasValue)
                    query = query.Where(t => t.Category == queryParams.Category.Value);

                if (queryParams.Color.HasValue)
                    query = query.Where(t => t.Color == queryParams.Color.Value);

                if (!string.IsNullOrEmpty(queryParams.AssignedTo))
                    query = query.Where(t => t.AssignedTo.Contains(queryParams.AssignedTo));

                if (queryParams.DueDateFrom.HasValue)
                    query = query.Where(t => t.DueDate >= queryParams.DueDateFrom.Value);

                if (queryParams.DueDateTo.HasValue)
                    query = query.Where(t => t.DueDate <= queryParams.DueDateTo.Value);

                if (queryParams.MinDuration.HasValue)
                    query = query.Where(t => t.Duration >= queryParams.MinDuration.Value);

                if (queryParams.MaxDuration.HasValue)
                    query = query.Where(t => t.Duration <= queryParams.MaxDuration.Value);

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "name" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Name)
                        : query.OrderBy(t => t.Name),
                    "priority" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Priority)
                        : query.OrderBy(t => t.Priority),
                    "status" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Status)
                        : query.OrderBy(t => t.Status),
                    "duration" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Duration)
                        : query.OrderBy(t => t.Duration),
                    "assigned_to" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.AssignedTo)
                        : query.OrderBy(t => t.AssignedTo),
                    "category" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.Category)
                        : query.OrderBy(t => t.Category),
                    "completed_date" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.CompletedDate)
                        : query.OrderBy(t => t.CompletedDate),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(t => t.DueDate)
                        : query.OrderBy(t => t.DueDate)
                };

                // Get total count before pagination
                var total = await query.CountAsync();

                // Apply pagination
                var tasks = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                // Map to DTOs
                var tasksDto = tasks.Select(t => new TaskDto
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    Description = t.Description,
                    Priority = t.Priority,
                    DueDate = t.DueDate.ToString("M/d/yyyy"),
                    AssignedTo = t.AssignedTo,
                    Status = t.Status,
                    Notes = t.Notes,
                    Category = t.Category,
                    Duration = t.Duration,
                    CompletedDate = t.CompletedDate?.ToString("M/d/yyyy") ?? string.Empty,
                    Color = t.Color
                }).ToList();

                return new ApiResponse<List<TaskDto>>
                {
                    Data = tasksDto,
                    Meta = new PaginationMeta
                    {
                        Page = queryParams.Page,
                        Limit = queryParams.Limit,
                        Total = total,
                        TotalPages = (int)Math.Ceiling((double)total / queryParams.Limit)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks data");
                throw;
            }
        }

        public async Task<TaskDto?> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var taskId))
                    return null;

                var task = await _context.AntdTasks.FindAsync(taskId);
                if (task == null)
                    return null;

                return new TaskDto
                {
                    Id = task.Id.ToString(),
                    Name = task.Name,
                    Description = task.Description,
                    Priority = task.Priority,
                    DueDate = task.DueDate.ToString("M/d/yyyy"),
                    AssignedTo = task.AssignedTo,
                    Status = task.Status,
                    Notes = task.Notes,
                    Category = task.Category,
                    Duration = task.Duration,
                    CompletedDate = task.CompletedDate?.ToString("M/d/yyyy") ?? string.Empty,
                    Color = task.Color
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task {TaskId}", id);
                throw;
            }
        }

        public async Task<TaskDto> CreateAsync(TaskDto taskDto)
        {
            try
            {
                var task = new AntdTask
                {
                    Id = Guid.NewGuid(),
                    Name = taskDto.Name,
                    Description = taskDto.Description,
                    Priority = taskDto.Priority,
                    DueDate = DateTime.Parse(taskDto.DueDate),
                    AssignedTo = taskDto.AssignedTo,
                    Status = taskDto.Status,
                    Notes = taskDto.Notes,
                    Category = taskDto.Category,
                    Duration = taskDto.Duration,
                    CompletedDate = string.IsNullOrEmpty(taskDto.CompletedDate) ? null : DateTime.Parse(taskDto.CompletedDate),
                    Color = taskDto.Color,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdTasks.Add(task);
                await _context.SaveChangesAsync();

                taskDto.Id = task.Id.ToString();
                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                throw;
            }
        }

        public async Task<TaskDto?> UpdateAsync(string id, TaskDto taskDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var taskId))
                    return null;

                var task = await _context.AntdTasks.FindAsync(taskId);
                if (task == null)
                    return null;

                task.Name = taskDto.Name;
                task.Description = taskDto.Description;
                task.Priority = taskDto.Priority;
                task.DueDate = DateTime.Parse(taskDto.DueDate);
                task.AssignedTo = taskDto.AssignedTo;
                task.Status = taskDto.Status;
                task.Notes = taskDto.Notes;
                task.Category = taskDto.Category;
                task.Duration = taskDto.Duration;
                task.CompletedDate = string.IsNullOrEmpty(taskDto.CompletedDate) ? null : DateTime.Parse(taskDto.CompletedDate);
                task.Color = taskDto.Color;
                task.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                taskDto.Id = task.Id.ToString();
                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task {TaskId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var taskId))
                    return false;

                var task = await _context.AntdTasks.FindAsync(taskId);
                if (task == null)
                    return false;

                _context.AntdTasks.Remove(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task {TaskId}", id);
                throw;
            }
        }
    }
}
