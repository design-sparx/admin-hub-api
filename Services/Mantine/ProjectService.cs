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

        public async Task<ProjectListResponse> GetAllAsync(ProjectQueryParams queryParams)
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

                return new ProjectListResponse
                {
                    Succeeded = true,
                    Data = projectsDto,
                    Message = "Projects retrieved successfully",
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

        public async Task<ProjectResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new ProjectResponse
                    {
                        Succeeded = false,
                        Message = "Invalid project ID format"
                    };
                }

                var project = await _context.Projects.FindAsync(guidId);
                if (project == null)
                {
                    return new ProjectResponse
                    {
                        Succeeded = false,
                        Message = "Project not found"
                    };
                }

                var projectDto = new ProjectDto
                {
                    Id = project.Id.ToString(),
                    Name = project.Name,
                    StartDate = project.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = project.EndDate.ToString("yyyy-MM-dd"),
                    State = project.State,
                    Assignee = project.Assignee
                };

                return new ProjectResponse
                {
                    Succeeded = true,
                    Data = projectDto,
                    Message = "Project retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project with ID {ProjectId}", id);
                throw;
            }
        }

        public async Task<ProjectCreateResponse> CreateAsync(ProjectDto projectDto)
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

                var createdProjectDto = new ProjectDto
                {
                    Id = project.Id.ToString(),
                    Name = project.Name,
                    StartDate = project.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = project.EndDate.ToString("yyyy-MM-dd"),
                    State = project.State,
                    Assignee = project.Assignee
                };

                return new ProjectCreateResponse
                {
                    Succeeded = true,
                    Data = createdProjectDto,
                    Message = "Project created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new project");
                throw;
            }
        }

        public async Task<ProjectUpdateResponse> UpdateAsync(string id, ProjectDto projectDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new ProjectUpdateResponse
                    {
                        Succeeded = false,
                        Message = "Invalid project ID format"
                    };
                }

                var project = await _context.Projects.FindAsync(guidId);
                if (project == null)
                {
                    return new ProjectUpdateResponse
                    {
                        Succeeded = false,
                        Message = "Project not found"
                    };
                }

                project.Name = projectDto.Name;
                project.StartDate = DateTime.Parse(projectDto.StartDate);
                project.EndDate = DateTime.Parse(projectDto.EndDate);
                project.State = projectDto.State;
                project.Assignee = projectDto.Assignee;
                project.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var updatedProjectDto = new ProjectDto
                {
                    Id = project.Id.ToString(),
                    Name = project.Name,
                    StartDate = project.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = project.EndDate.ToString("yyyy-MM-dd"),
                    State = project.State,
                    Assignee = project.Assignee
                };

                return new ProjectUpdateResponse
                {
                    Succeeded = true,
                    Data = updatedProjectDto,
                    Message = "Project updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project with ID {ProjectId}", id);
                throw;
            }
        }

        public async Task<ProjectDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new ProjectDeleteResponse
                    {
                        Succeeded = false,
                        Message = "Invalid project ID format"
                    };
                }

                var project = await _context.Projects.FindAsync(guidId);
                if (project == null)
                {
                    return new ProjectDeleteResponse
                    {
                        Succeeded = false,
                        Message = "Project not found"
                    };
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return new ProjectDeleteResponse
                {
                    Succeeded = true,
                    Message = "Project deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project with ID {ProjectId}", id);
                throw;
            }
        }
    }
}