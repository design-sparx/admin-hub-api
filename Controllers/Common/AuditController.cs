using AdminHubApi.Constants;
using AdminHubApi.Interfaces;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Common
{
    [Route("/api/v1/audit")]
    [Tags("Audit Logs")]
    [PermissionAuthorize(Permissions.Admin.SystemSettings)]
    public class AuditController : BaseApiController
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService, ILogger<AuditController> logger)
            : base(logger)
        {
            _auditService = auditService;
        }

        /// <summary>
        /// Get audit logs with filtering
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAuditLogs(
            [FromQuery] string? entityName = null,
            [FromQuery] string? entityId = null,
            [FromQuery] string? userId = null,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 50)
        {
            try
            {
                var auditLogs = await _auditService.GetAuditLogsAsync(entityName, entityId, userId, page, limit);
                var totalCount = await _auditService.GetAuditLogsCountAsync(entityName, entityId, userId);

                return SuccessResponse(new
                {
                    data = auditLogs,
                    meta = new
                    {
                        page,
                        limit,
                        total = totalCount,
                        totalPages = (int)Math.Ceiling((double)totalCount / limit)
                    }
                }, "Audit logs retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving audit logs");
                return ErrorResponse("Failed to retrieve audit logs", 500);
            }
        }

        /// <summary>
        /// Get audit logs for a specific invoice
        /// </summary>
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceAuditLogs(string invoiceId)
        {
            try
            {
                var auditLogs = await _auditService.GetAuditLogsAsync("Invoice", invoiceId, null, 1, 100);
                return SuccessResponse(auditLogs, $"Audit logs for invoice {invoiceId} retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving audit logs for invoice {InvoiceId}", invoiceId);
                return ErrorResponse("Failed to retrieve invoice audit logs", 500);
            }
        }
    }
}