using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdProjectService : IAntdProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdProjectService> _logger;

        public AntdProjectService(ApplicationDbContext context, ILogger<AntdProjectService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdProjectListResponse> GetAllAsync(AntdProjectQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdProjects.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Status))
                {
                    query = query.Where(p => p.Status.ToLower() == queryParams.Status.ToLower());
                }

                if (!string.IsNullOrEmpty(queryParams.Priority))
                {
                    query = query.Where(p => p.Priority.ToLower() == queryParams.Priority.ToLower());
                }

                if (!string.IsNullOrEmpty(queryParams.ProjectManager))
                {
                    query = query.Where(p => p.ProjectManager.Contains(queryParams.ProjectManager));
                }

                if (!string.IsNullOrEmpty(queryParams.ClientName))
                {
                    query = query.Where(p => p.ClientName.Contains(queryParams.ClientName));
                }

                if (!string.IsNullOrEmpty(queryParams.ProjectType))
                {
                    query = query.Where(p => p.ProjectType.ToLower() == queryParams.ProjectType.ToLower());
                }

                if (!string.IsNullOrEmpty(queryParams.ProjectCategory))
                {
                    query = query.Where(p => p.ProjectCategory.ToLower() == queryParams.ProjectCategory.ToLower());
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
                    "projectname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.ProjectName)
                        : query.OrderBy(p => p.ProjectName),
                    "enddate" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.EndDate)
                        : query.OrderBy(p => p.EndDate),
                    "status" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Status)
                        : query.OrderBy(p => p.Status),
                    "priority" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Priority)
                        : query.OrderBy(p => p.Priority),
                    "projectmanager" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.ProjectManager)
                        : query.OrderBy(p => p.ProjectManager),
                    "clientname" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.ClientName)
                        : query.OrderBy(p => p.ClientName),
                    "teamsize" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.TeamSize)
                        : query.OrderBy(p => p.TeamSize),
                    "budget" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Budget)
                        : query.OrderBy(p => p.Budget),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.StartDate)
                        : query.OrderBy(p => p.StartDate)
                };

                var total = await query.CountAsync();
                var projects = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var projectsDto = projects.Select(MapToDto).ToList();

                return new AntdProjectListResponse
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
                _logger.LogError(ex, "Error retrieving Antd projects data");
                throw;
            }
        }

        public async Task<AntdProjectResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new AntdProjectResponse
                    {
                        Succeeded = false,
                        Message = "Invalid project ID format"
                    };
                }

                var project = await _context.AntdProjects.FindAsync(guidId);
                if (project == null)
                {
                    return new AntdProjectResponse
                    {
                        Succeeded = false,
                        Message = "Project not found"
                    };
                }

                return new AntdProjectResponse
                {
                    Succeeded = true,
                    Data = MapToDto(project),
                    Message = "Project retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd project with ID {ProjectId}", id);
                throw;
            }
        }

        public async Task<AntdProjectCreateResponse> CreateAsync(AntdProjectDto projectDto)
        {
            try
            {
                var project = new AntdProject
                {
                    Id = Guid.NewGuid(),
                    ProjectName = projectDto.ProjectName,
                    StartDate = DateTime.Parse(projectDto.StartDate).ToUniversalTime(),
                    EndDate = DateTime.Parse(projectDto.EndDate).ToUniversalTime(),
                    Budget = projectDto.Budget,
                    ProjectManager = projectDto.ProjectManager,
                    ClientName = projectDto.ClientName,
                    Status = projectDto.Status,
                    Priority = projectDto.Priority,
                    TeamSize = projectDto.TeamSize,
                    ProjectDescription = projectDto.ProjectDescription,
                    ProjectLocation = projectDto.ProjectLocation,
                    ProjectType = projectDto.ProjectType,
                    ProjectCategory = projectDto.ProjectCategory,
                    ProjectDuration = projectDto.ProjectDuration,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdProjects.Add(project);
                await _context.SaveChangesAsync();

                return new AntdProjectCreateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(project),
                    Message = "Project created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new Antd project");
                throw;
            }
        }

        public async Task<AntdProjectUpdateResponse> UpdateAsync(string id, AntdProjectDto projectDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new AntdProjectUpdateResponse
                    {
                        Succeeded = false,
                        Message = "Invalid project ID format"
                    };
                }

                var project = await _context.AntdProjects.FindAsync(guidId);
                if (project == null)
                {
                    return new AntdProjectUpdateResponse
                    {
                        Succeeded = false,
                        Message = "Project not found"
                    };
                }

                project.ProjectName = projectDto.ProjectName;
                project.StartDate = DateTime.Parse(projectDto.StartDate).ToUniversalTime();
                project.EndDate = DateTime.Parse(projectDto.EndDate).ToUniversalTime();
                project.Budget = projectDto.Budget;
                project.ProjectManager = projectDto.ProjectManager;
                project.ClientName = projectDto.ClientName;
                project.Status = projectDto.Status;
                project.Priority = projectDto.Priority;
                project.TeamSize = projectDto.TeamSize;
                project.ProjectDescription = projectDto.ProjectDescription;
                project.ProjectLocation = projectDto.ProjectLocation;
                project.ProjectType = projectDto.ProjectType;
                project.ProjectCategory = projectDto.ProjectCategory;
                project.ProjectDuration = projectDto.ProjectDuration;
                project.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdProjectUpdateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(project),
                    Message = "Project updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd project with ID {ProjectId}", id);
                throw;
            }
        }

        public async Task<AntdProjectDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                {
                    return new AntdProjectDeleteResponse
                    {
                        Succeeded = false,
                        Message = "Invalid project ID format"
                    };
                }

                var project = await _context.AntdProjects.FindAsync(guidId);
                if (project == null)
                {
                    return new AntdProjectDeleteResponse
                    {
                        Succeeded = false,
                        Message = "Project not found"
                    };
                }

                _context.AntdProjects.Remove(project);
                await _context.SaveChangesAsync();

                return new AntdProjectDeleteResponse
                {
                    Succeeded = true,
                    Message = "Project deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd project with ID {ProjectId}", id);
                throw;
            }
        }

        private static AntdProjectDto MapToDto(AntdProject project)
        {
            return new AntdProjectDto
            {
                ProjectId = project.Id.ToString(),
                ProjectName = project.ProjectName,
                StartDate = project.StartDate.ToString("yyyy-MM-dd"),
                EndDate = project.EndDate.ToString("yyyy-MM-dd"),
                Budget = project.Budget,
                ProjectManager = project.ProjectManager,
                ClientName = project.ClientName,
                Status = project.Status,
                Priority = project.Priority,
                TeamSize = project.TeamSize,
                ProjectDescription = project.ProjectDescription,
                ProjectLocation = project.ProjectLocation,
                ProjectType = project.ProjectType,
                ProjectCategory = project.ProjectCategory,
                ProjectDuration = project.ProjectDuration
            };
        }
    }
}
