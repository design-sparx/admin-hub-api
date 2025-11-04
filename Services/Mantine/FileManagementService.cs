using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Enums.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdminHubApi.Services.Mantine
{
    public class FileManagementService : IFileManagementService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FileManagementService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileManagementService(ApplicationDbContext context, ILogger<FileManagementService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<FileDto>>> GetFilesAsync(FileQueryParams queryParams)
        {
            try
            {
                var query = _context.Files.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.FolderId))
                {
                    if (Guid.TryParse(queryParams.FolderId, out var folderGuid))
                    {
                        query = query.Where(f => f.FolderId == folderGuid);
                    }
                }

                if (!string.IsNullOrEmpty(queryParams.FileType))
                {
                    query = query.Where(f => f.Type.Contains(queryParams.FileType));
                }

                if (!string.IsNullOrEmpty(queryParams.OwnerId))
                {
                    if (Guid.TryParse(queryParams.OwnerId, out var ownerGuid))
                    {
                        query = query.Where(f => f.OwnerId == ownerGuid);
                    }
                }

                var total = await query.CountAsync();
                var files = await query
                    .OrderByDescending(f => f.CreatedAt)
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var filesDto = files.Select(f => new FileDto
                {
                    Id = f.Id.ToString(),
                    Name = f.Name,
                    Size = f.Size,
                    Type = f.Type,
                    Path = f.Path,
                    FolderId = f.FolderId?.ToString(),
                    OwnerId = f.OwnerId.ToString()
                }).ToList();

                return new ApiResponse<List<FileDto>>
                {
                    Data = filesDto,
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
                _logger.LogError(ex, "Error retrieving files");
                throw;
            }
        }

        public async Task<ApiResponse<List<FolderDto>>> GetFoldersAsync(FolderQueryParams queryParams)
        {
            try
            {
                var query = _context.Folders.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.ParentId))
                {
                    if (Guid.TryParse(queryParams.ParentId, out var parentGuid))
                    {
                        query = query.Where(f => f.ParentId == parentGuid);
                    }
                }

                if (!string.IsNullOrEmpty(queryParams.OwnerId))
                {
                    if (Guid.TryParse(queryParams.OwnerId, out var ownerGuid))
                    {
                        query = query.Where(f => f.OwnerId == ownerGuid);
                    }
                }

                var total = await query.CountAsync();
                var folders = await query
                    .OrderByDescending(f => f.CreatedAt)
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var foldersDto = folders.Select(f => new FolderDto
                {
                    Id = f.Id.ToString(),
                    Name = f.Name,
                    Path = f.Path,
                    ParentId = f.ParentId?.ToString(),
                    OwnerId = f.OwnerId.ToString()
                }).ToList();

                return new ApiResponse<List<FolderDto>>
                {
                    Data = foldersDto,
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
                _logger.LogError(ex, "Error retrieving folders");
                throw;
            }
        }

        public async Task<ApiResponse<List<FileActivityDto>>> GetFileActivitiesAsync(int page = 1, int limit = 20)
        {
            try
            {
                var query = _context.FileActivities.AsQueryable();

                var total = await query.CountAsync();
                var activities = await query
                    .OrderByDescending(fa => fa.CreatedAt)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                var activitiesDto = activities.Select(fa => new FileActivityDto
                {
                    Id = fa.Id.ToString(),
                    FileId = fa.FileId.ToString(),
                    UserId = fa.UserId.ToString(),
                    Action = fa.Action,
                    Description = fa.Description,
                    CreatedAt = fa.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                }).ToList();

                return new ApiResponse<List<FileActivityDto>>
                {
                    Data = activitiesDto,
                    Meta = new PaginationMeta
                    {
                        Page = page,
                        Limit = limit,
                        Total = total,
                        TotalPages = (int)Math.Ceiling((double)total / limit)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving file activities");
                throw;
            }
        }

        public async Task<FileDto?> GetFileByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var file = await _context.Files.FindAsync(guidId);
                if (file == null) return null;

                return new FileDto
                {
                    Id = file.Id.ToString(),
                    Name = file.Name,
                    Size = file.Size,
                    Type = file.Type,
                    Path = file.Path,
                    FolderId = file.FolderId?.ToString(),
                    OwnerId = file.OwnerId.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving file with ID {FileId}", id);
                throw;
            }
        }

        public async Task<FolderDto?> GetFolderByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var folder = await _context.Folders.FindAsync(guidId);
                if (folder == null) return null;

                return new FolderDto
                {
                    Id = folder.Id.ToString(),
                    Name = folder.Name,
                    Path = folder.Path,
                    ParentId = folder.ParentId?.ToString(),
                    OwnerId = folder.OwnerId.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving folder with ID {FolderId}", id);
                throw;
            }
        }

        public async Task<FileDto> CreateFileAsync(FileDto fileDto)
        {
            try
            {
                var file = new Files
                {
                    Id = Guid.NewGuid(),
                    Name = fileDto.Name,
                    Size = fileDto.Size,
                    Type = fileDto.Type,
                    Path = fileDto.Path,
                    FolderId = string.IsNullOrEmpty(fileDto.FolderId) ? null : Guid.Parse(fileDto.FolderId),
                    OwnerId = Guid.Parse(fileDto.OwnerId),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Files.Add(file);
                await _context.SaveChangesAsync();

                // Log file creation activity
                await LogFileActivityAsync(file.Id, FileAction.Created, $"File '{file.Name}' was created");

                fileDto.Id = file.Id.ToString();
                return fileDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new file");
                throw;
            }
        }

        public async Task<FolderDto> CreateFolderAsync(FolderDto folderDto)
        {
            try
            {
                var folder = new Folders
                {
                    Id = Guid.NewGuid(),
                    Name = folderDto.Name,
                    Path = folderDto.Path,
                    ParentId = string.IsNullOrEmpty(folderDto.ParentId) ? null : Guid.Parse(folderDto.ParentId),
                    OwnerId = Guid.Parse(folderDto.OwnerId),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Folders.Add(folder);
                await _context.SaveChangesAsync();

                folderDto.Id = folder.Id.ToString();
                return folderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new folder");
                throw;
            }
        }

        public async Task<FileDto?> UpdateFileAsync(string id, FileDto fileDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var file = await _context.Files.FindAsync(guidId);
                if (file == null) return null;

                file.Name = fileDto.Name;
                file.Size = fileDto.Size;
                file.Type = fileDto.Type;
                file.Path = fileDto.Path;
                file.FolderId = string.IsNullOrEmpty(fileDto.FolderId) ? null : Guid.Parse(fileDto.FolderId);
                file.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                // Log file update activity
                await LogFileActivityAsync(file.Id, FileAction.Updated, $"File '{file.Name}' was updated");

                return fileDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating file with ID {FileId}", id);
                throw;
            }
        }

        public async Task<FolderDto?> UpdateFolderAsync(string id, FolderDto folderDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return null;

                var folder = await _context.Folders.FindAsync(guidId);
                if (folder == null) return null;

                folder.Name = folderDto.Name;
                folder.Path = folderDto.Path;
                folder.ParentId = string.IsNullOrEmpty(folderDto.ParentId) ? null : Guid.Parse(folderDto.ParentId);
                folder.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return folderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating folder with ID {FolderId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var file = await _context.Files.FindAsync(guidId);
                if (file == null) return false;

                // Log file deletion activity before deleting
                await LogFileActivityAsync(file.Id, FileAction.Deleted, $"File '{file.Name}' was deleted");

                _context.Files.Remove(file);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file with ID {FileId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteFolderAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return false;

                var folder = await _context.Folders.FindAsync(guidId);
                if (folder == null) return false;

                _context.Folders.Remove(folder);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting folder with ID {FolderId}", id);
                throw;
            }
        }

        private async Task LogFileActivityAsync(Guid fileId, FileAction action, string description)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
                    return;

                var activity = new FileActivities
                {
                    Id = Guid.NewGuid(),
                    FileId = fileId,
                    UserId = userGuid,
                    Action = action,
                    Description = description,
                    CreatedAt = DateTime.UtcNow
                };

                _context.FileActivities.Add(activity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging file activity");
            }
        }

        private string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}