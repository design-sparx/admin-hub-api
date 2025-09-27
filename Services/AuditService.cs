using AdminHubApi.Data;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace AdminHubApi.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuditService> _logger;

        public AuditService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
                           ILogger<AuditService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task LogAsync(string entityName, string entityId, string action, string endpoint,
                                  string httpMethod, string? oldValues = null, string? newValues = null)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null) return;

                var userId = httpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous";
                var userEmail = httpContext.User?.FindFirst(ClaimTypes.Email)?.Value;
                var ipAddress = GetClientIpAddress(httpContext);
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

                var auditLog = new AuditLog
                {
                    EntityName = entityName,
                    EntityId = entityId,
                    Action = action.ToUpper(),
                    UserId = userId,
                    UserEmail = userEmail,
                    Endpoint = endpoint,
                    HttpMethod = httpMethod.ToUpper(),
                    IpAddress = ipAddress,
                    UserAgent = userAgent.Length > 500 ? userAgent[..500] : userAgent,
                    OldValues = oldValues,
                    NewValues = newValues,
                    Timestamp = DateTime.UtcNow
                };

                _context.AuditLogs.Add(auditLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log audit trail for {EntityName} {EntityId}", entityName, entityId);
            }
        }

        public async Task<List<AuditLog>> GetAuditLogsAsync(string? entityName = null, string? entityId = null,
                                                           string? userId = null, int page = 1, int limit = 50)
        {
            var query = _context.AuditLogs.Include(a => a.User).AsQueryable();

            if (!string.IsNullOrEmpty(entityName))
                query = query.Where(a => a.EntityName == entityName);

            if (!string.IsNullOrEmpty(entityId))
                query = query.Where(a => a.EntityId == entityId);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(a => a.UserId == userId);

            return await query
                .OrderByDescending(a => a.Timestamp)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> GetAuditLogsCountAsync(string? entityName = null, string? entityId = null, string? userId = null)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(entityName))
                query = query.Where(a => a.EntityName == entityName);

            if (!string.IsNullOrEmpty(entityId))
                query = query.Where(a => a.EntityId == entityId);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(a => a.UserId == userId);

            return await query.CountAsync();
        }

        private string? GetClientIpAddress(HttpContext context)
        {
            // Check for forwarded IP first (in case of proxy/load balancer)
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            return context.Connection.RemoteIpAddress?.ToString();
        }
    }

    public static class AuditActions
    {
        public const string CREATE = "CREATE";
        public const string READ = "READ";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
        public const string LIST = "LIST";
    }
}