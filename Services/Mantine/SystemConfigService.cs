using AdminHubApi.Data;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class SystemConfigService : ISystemConfigService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SystemConfigService> _logger;

        public SystemConfigService(ApplicationDbContext context, ILogger<SystemConfigService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResponse<List<LanguageDto>>> GetLanguagesAsync()
        {
            try
            {
                var languages = await _context.Languages
                    .OrderBy(l => l.Name)
                    .ToListAsync();

                var languagesDto = languages.Select(l => new LanguageDto
                {
                    Code = l.Code,
                    Name = l.Name,
                    NativeName = l.NativeName,
                    IsActive = l.IsActive
                }).ToList();

                return new ApiResponse<List<LanguageDto>>
                {
                    Data = languagesDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving languages");
                throw;
            }
        }

        public async Task<ApiResponse<List<CountryDto>>> GetCountriesAsync()
        {
            try
            {
                var countries = await _context.Countries
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                var countriesDto = countries.Select(c => new CountryDto
                {
                    Code = c.Code,
                    Name = c.Name,
                    Continent = c.Continent,
                    Population = c.Population
                }).ToList();

                return new ApiResponse<List<CountryDto>>
                {
                    Data = countriesDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving countries");
                throw;
            }
        }

        public async Task<ApiResponse<List<TrafficDto>>> GetTrafficAsync(TrafficQueryParams queryParams)
        {
            try
            {
                var query = _context.Traffic.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(queryParams.Source))
                {
                    query = query.Where(t => t.Source.Contains(queryParams.Source));
                }

                if (queryParams.DateFrom.HasValue)
                {
                    query = query.Where(t => t.Date >= queryParams.DateFrom.Value);
                }

                if (queryParams.DateTo.HasValue)
                {
                    query = query.Where(t => t.Date <= queryParams.DateTo.Value);
                }

                var total = await query.CountAsync();
                var traffic = await query
                    .OrderByDescending(t => t.Date)
                    .Skip((queryParams.Page - 1) * queryParams.Limit)
                    .Take(queryParams.Limit)
                    .ToListAsync();

                var trafficDto = traffic.Select(t => new TrafficDto
                {
                    Id = t.Id.ToString(),
                    Source = t.Source,
                    Visitors = t.Visitors,
                    Pageviews = t.Pageviews,
                    BounceRate = t.BounceRate,
                    AvgSessionDuration = t.AvgSessionDuration,
                    Date = t.Date.ToString("yyyy-MM-dd")
                }).ToList();

                return new ApiResponse<List<TrafficDto>>
                {
                    Data = trafficDto,
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
                _logger.LogError(ex, "Error retrieving traffic data");
                throw;
            }
        }
    }
}