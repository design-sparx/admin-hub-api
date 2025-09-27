using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces
{
    public interface IAuditService
    {
        Task LogAsync(string entityName, string entityId, string action, string endpoint,
                     string httpMethod, string? oldValues = null, string? newValues = null);

        Task<List<AuditLog>> GetAuditLogsAsync(string? entityName = null, string? entityId = null,
                                             string? userId = null, int page = 1, int limit = 50);

        Task<int> GetAuditLogsCountAsync(string? entityName = null, string? entityId = null, string? userId = null);
    }
}