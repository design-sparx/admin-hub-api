using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Interfaces.Antd;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Antd
{
    public class AntdLicenseService : IAntdLicenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AntdLicenseService> _logger;

        public AntdLicenseService(ApplicationDbContext context, ILogger<AntdLicenseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AntdLicenseListResponse> GetAllAsync(AntdLicenseQueryParams queryParams)
        {
            try
            {
                var query = _context.AntdLicenses.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Title))
                    query = query.Where(l => l.Title.ToLower() == queryParams.Title.ToLower());

                if (!string.IsNullOrEmpty(queryParams.SearchTerm))
                {
                    var searchLower = queryParams.SearchTerm.ToLower();
                    query = query.Where(l =>
                        l.Title.ToLower().Contains(searchLower) ||
                        l.Description.ToLower().Contains(searchLower));
                }

                // Apply sorting
                query = queryParams.SortBy.ToLower() switch
                {
                    "title" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(l => l.Title)
                        : query.OrderBy(l => l.Title),
                    "createdat" => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(l => l.CreatedAt)
                        : query.OrderBy(l => l.CreatedAt),
                    _ => queryParams.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(l => l.Title)
                        : query.OrderBy(l => l.Title)
                };

                var total = await query.CountAsync();
                var licenses = await query
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                return new AntdLicenseListResponse
                {
                    Succeeded = true,
                    Data = licenses.Select(MapToDto).ToList(),
                    Message = "Licenses retrieved successfully",
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
                _logger.LogError(ex, "Error retrieving Antd licenses");
                throw;
            }
        }

        public async Task<AntdLicenseResponse> GetByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdLicenseResponse { Succeeded = false, Message = "Invalid license ID format" };

                var license = await _context.AntdLicenses.FindAsync(guidId);
                if (license == null)
                    return new AntdLicenseResponse { Succeeded = false, Message = "License not found" };

                return new AntdLicenseResponse
                {
                    Succeeded = true,
                    Data = MapToDto(license),
                    Message = "License retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd license {LicenseId}", id);
                throw;
            }
        }

        public async Task<AntdLicenseResponse> GetByTitleAsync(string title)
        {
            try
            {
                var license = await _context.AntdLicenses
                    .FirstOrDefaultAsync(l => l.Title.ToLower() == title.ToLower());

                if (license == null)
                    return new AntdLicenseResponse { Succeeded = false, Message = "License not found" };

                return new AntdLicenseResponse
                {
                    Succeeded = true,
                    Data = MapToDto(license),
                    Message = "License retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd license {Title}", title);
                throw;
            }
        }

        public async Task<AntdLicenseCreateResponse> CreateAsync(AntdLicenseDto licenseDto)
        {
            try
            {
                var license = new AntdLicense
                {
                    Id = Guid.NewGuid(),
                    Title = licenseDto.Title,
                    Description = licenseDto.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.AntdLicenses.Add(license);
                await _context.SaveChangesAsync();

                return new AntdLicenseCreateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(license),
                    Message = "License created successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd license");
                throw;
            }
        }

        public async Task<AntdLicenseUpdateResponse> UpdateAsync(string id, AntdLicenseDto licenseDto)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdLicenseUpdateResponse { Succeeded = false, Message = "Invalid license ID format" };

                var license = await _context.AntdLicenses.FindAsync(guidId);
                if (license == null)
                    return new AntdLicenseUpdateResponse { Succeeded = false, Message = "License not found" };

                license.Title = licenseDto.Title;
                license.Description = licenseDto.Description;
                license.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new AntdLicenseUpdateResponse
                {
                    Succeeded = true,
                    Data = MapToDto(license),
                    Message = "License updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd license {LicenseId}", id);
                throw;
            }
        }

        public async Task<AntdLicenseDeleteResponse> DeleteAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var guidId))
                    return new AntdLicenseDeleteResponse { Succeeded = false, Message = "Invalid license ID format" };

                var license = await _context.AntdLicenses.FindAsync(guidId);
                if (license == null)
                    return new AntdLicenseDeleteResponse { Succeeded = false, Message = "License not found" };

                _context.AntdLicenses.Remove(license);
                await _context.SaveChangesAsync();

                return new AntdLicenseDeleteResponse
                {
                    Succeeded = true,
                    Message = "License deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd license {LicenseId}", id);
                throw;
            }
        }

        private static AntdLicenseDto MapToDto(AntdLicense license)
        {
            return new AntdLicenseDto
            {
                Id = license.Id.ToString(),
                Title = license.Title,
                Description = license.Description
            };
        }
    }
}
