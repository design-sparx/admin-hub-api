using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(ApplicationDbContext context, ILogger<ProjectService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<ProjectDto>>> GetAllAsync(ProjectQueryParams queryParams)
        {
            try
            {
                var query = _context.Projects.AsQueryable();

                // Apply filters
                if (queryParams.State.HasValue)
                {
                    query = query.Where(p => p.State == queryParams.State.Value);
                }

                if (!string.IsNullOrEmpty(queryParams.Assignee))
                {
                    query = query.Where(p => p.Assignee.Contains(queryParams.Assignee));
                }

                if (queryParams.StartDateFrom.HasValue)
                {
                    query = query.Where(p => p.StartDate >= queryParams.StartDateFrom.Value);
                }

                if (queryParams.StartDateTo.HasValue)
                {
                    query = query.Where(p => p.StartDate <= queryParams.StartDateTo.Value);
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "name" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name),
                    "enddate" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.EndDate)
                        : query.OrderBy(p => p.EndDate),
                    "state" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.State)
                        : query.OrderBy(p => p.State),
                    "assignee" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Assignee)
                        : query.OrderBy(p => p.Assignee),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.StartDate)
                        : query.OrderBy(p => p.StartDate)
                };

                var total = await query.CountAsync();
                var projects = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var projectsDto = projects.Select(p => new ProjectDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    StartDate = p.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = p.EndDate.ToString("yyyy-MM-dd"),
                    State = p.State,
                    Assignee = p.Assignee
                }).ToList();

                return new ApiResponse<List<ProjectDto>>
                {
                    Data = projectsDto,
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
                _logger.LogError(ex, "Error retrieving projects data");
                throw;
            }
        }

        public async Task<ProjectDto?> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var project = await _context.Projects.FindAsync(guidId);
                if (project == null) return null;

                return new ProjectDto
                {
                    Id = project.Id.ToString(),
                    Name = project.Name,
                    StartDate = project.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = project.EndDate.ToString("yyyy-MM-dd"),
                    State = project.State,
                    Assignee = project.Assignee
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project with ID {ProjectId}", id);
                throw;
            }
        }

        public async Task<ProjectDto> CreateAsync(ProjectDto projectDto)
        {
            try
            {
                var project = new Projects
                {
                    Id = Guid.NewGuid(),
                    Name = projectDto.Name,
                    StartDate = DateTime.Parse(projectDto.StartDate),
                    EndDate = DateTime.Parse(projectDto.EndDate),
                    State = projectDto.State,
                    Assignee = projectDto.Assignee,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                projectDto.Id = project.Id.ToString();
                return projectDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new project");
                throw;
            }
        }

        public async Task<ProjectDto?> UpdateAsync(string id, ProjectDto projectDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var project = await _context.Projects.FindAsync(guidId);
                if (project == null) return null;

                project.Name = projectDto.Name;
                project.StartDate = DateTime.Parse(projectDto.StartDate);
                project.EndDate = DateTime.Parse(projectDto.EndDate);
                project.State = projectDto.State;
                project.Assignee = projectDto.Assignee;
                project.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return projectDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project with ID {ProjectId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var project = await _context.Projects.FindAsync(guidId);
                if (project == null) return false;

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project with ID {ProjectId}", id);
                throw;
            }
        }
    }
}