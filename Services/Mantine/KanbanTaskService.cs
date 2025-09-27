using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;
using TaskStatus = AdminHubApi.Enums.Mantine.TaskStatus;

namespace AdminHubApi.Services.Mantine
{
    public class KanbanTaskService : IKanbanTaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<KanbanTaskService> _logger;

        public KanbanTaskService(ApplicationDbContext context, ILogger<KanbanTaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<KanbanTaskDto>>> GetAllAsync(KanbanTaskQueryParams queryParams)
        {
            try
            {
                var query = _context.KanbanTasks.AsQueryable();

                // Apply filters
                if (queryParams.Status.HasValue)
                {
                    query = query.Where(k => k.Status == queryParams.Status.Value);
                }

                if (!string.IsNullOrEmpty(queryParams.Title))
                {
                    query = query.Where(k => k.Title.Contains(queryParams.Title));
                }

                // Default sorting by creation date, newest first
                query = query.OrderByDescending(k => k.CreatedAt);

                var total = await query.CountAsync();
                var tasks = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var tasksDto = tasks.Select(k => new KanbanTaskDto
                {
                    Id = k.Id.ToString(),
                    Title = k.Title,
                    Status = k.Status,
                    Comments = k.Comments,
                    Users = k.Users
                }).ToList();

                return new ApiResponse<List<KanbanTaskDto>>
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
                _logger.LogError(ex, "Error retrieving kanban tasks data");
                throw;
            }
        }

        public async Task<KanbanTaskDto?> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var task = await _context.KanbanTasks.FindAsync(guidId);
                if (task == null) return null;

                return new KanbanTaskDto
                {
                    Id = task.Id.ToString(),
                    Title = task.Title,
                    Status = task.Status,
                    Comments = task.Comments,
                    Users = task.Users
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving kanban task with ID {TaskId}", id);
                throw;
            }
        }

        public async Task<KanbanTaskDto> CreateAsync(KanbanTaskDto taskDto)
        {
            try
            {
                var task = new KanbanTasks
                {
                    Id = Guid.NewGuid(),
                    Title = taskDto.Title,
                    Status = taskDto.Status,
                    Comments = taskDto.Comments,
                    Users = taskDto.Users,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.KanbanTasks.Add(task);
                await _context.SaveChangesAsync();

                taskDto.Id = task.Id.ToString();
                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new kanban task");
                throw;
            }
        }

        public async Task<KanbanTaskDto?> UpdateAsync(string id, KanbanTaskDto taskDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var task = await _context.KanbanTasks.FindAsync(guidId);
                if (task == null) return null;

                task.Title = taskDto.Title;
                task.Status = taskDto.Status;
                task.Comments = taskDto.Comments;
                task.Users = taskDto.Users;
                task.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return taskDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating kanban task with ID {TaskId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var task = await _context.KanbanTasks.FindAsync(guidId);
                if (task == null) return false;

                _context.KanbanTasks.Remove(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting kanban task with ID {TaskId}", id);
                throw;
            }
        }
    }
}