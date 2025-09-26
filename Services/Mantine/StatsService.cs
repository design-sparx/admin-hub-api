using AdminHubApi.Data;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services.Mantine
{
    public class StatsService : IStatsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatsService> _logger;

        public StatsService(ApplicationDbContext context, ILogger<StatsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<StatsGridResponse> GetStatsAsync()
        {
            try
            {
                var stats = await _context.DashboardStats
                    .OrderBy(s => s.Id)
                    .ToListAsync();

                var statsDto = stats.Select(s => new StatsDto
                {
                    Title = s.Title,
                    Icon = s.Icon,
                    Value = s.Value,
                    Diff = s.Diff,
                    Period = s.Period
                }).ToList();

                return new StatsGridResponse { Data = statsDto };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard stats");
                throw;
            }
        }

        public async Task<List<StatsDto>> GetAllStatsAsync()
        {
            try
            {
                var stats = await _context.DashboardStats
                    .OrderBy(s => s.Id)
                    .ToListAsync();

                return stats.Select(s => new StatsDto
                {
                    Title = s.Title,
                    Icon = s.Icon,
                    Value = s.Value,
                    Diff = s.Diff,
                    Period = s.Period
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all stats");
                throw;
            }
        }

        public async Task<StatsDto?> GetStatByIdAsync(int id)
        {
            try
            {
                var stat = await _context.DashboardStats.FindAsync(id);
                if (stat == null) return null;

                return new StatsDto
                {
                    Title = stat.Title,
                    Icon = stat.Icon,
                    Value = stat.Value,
                    Diff = stat.Diff,
                    Period = stat.Period
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving stat with ID {StatId}", id);
                throw;
            }
        }

        public async Task<StatsDto> CreateStatAsync(StatsDto statsDto)
        {
            try
            {
                var stat = new DashboardStats
                {
                    Title = statsDto.Title,
                    Icon = statsDto.Icon,
                    Value = statsDto.Value,
                    Diff = statsDto.Diff,
                    Period = statsDto.Period,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.DashboardStats.Add(stat);
                await _context.SaveChangesAsync();

                return statsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new stat");
                throw;
            }
        }

        public async Task<StatsDto?> UpdateStatAsync(int id, StatsDto statsDto)
        {
            try
            {
                var stat = await _context.DashboardStats.FindAsync(id);
                if (stat == null) return null;

                stat.Title = statsDto.Title;
                stat.Icon = statsDto.Icon;
                stat.Value = statsDto.Value;
                stat.Diff = statsDto.Diff;
                stat.Period = statsDto.Period;
                stat.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return statsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating stat with ID {StatId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteStatAsync(int id)
        {
            try
            {
                var stat = await _context.DashboardStats.FindAsync(id);
                if (stat == null) return false;

                _context.DashboardStats.Remove(stat);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting stat with ID {StatId}", id);
                throw;
            }
        }
    }
}