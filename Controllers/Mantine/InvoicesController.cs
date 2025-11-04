using AdminHubApi.Constants;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Security;
using AdminHubApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/invoices")]
    [Tags("Mantine - Invoices")]
    [PermissionAuthorize(Permissions.Personal.Invoices)]
    public class InvoicesController : MantineBaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IAuditService _auditService;

        public InvoicesController(IInvoiceService invoiceService, IAuditService auditService, ILogger<InvoicesController> logger)
            : base(logger)
        {
            _invoiceService = invoiceService;
            _auditService = auditService;
        }

        /// <summary>
        /// Get all invoices with pagination and filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(InvoiceListResponse), 200)]
        public async Task<IActionResult> GetAllInvoices([FromQuery] InvoiceQueryParams queryParams)
        {
            try
            {
                var response = await _invoiceService.GetAllAsync(queryParams);

                // Log the LIST action
                await _auditService.LogAsync("Invoice", "list", AuditActions.LIST,
                    Request.Path, Request.Method,
                    newValues: JsonSerializer.Serialize(new { queryParams, resultCount = response.Data?.Count ?? 0 }));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoices");
                return ErrorResponse("Failed to retrieve invoices", 500);
            }
        }

        /// <summary>
        /// Get invoice by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvoiceResponse), 200)]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            try
            {
                var response = await _invoiceService.GetByIdAsync(id);

                // Log the READ action
                await _auditService.LogAsync("Invoice", id, AuditActions.READ,
                    Request.Path, Request.Method,
                    newValues: JsonSerializer.Serialize(new { found = response.Succeeded }));

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice {InvoiceId}", id);
                return ErrorResponse("Failed to retrieve invoice", 500);
            }
        }

        /// <summary>
        /// Create new invoice
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(InvoiceCreateResponse), 201)]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _invoiceService.CreateAsync(invoiceDto);

                // Log the CREATE action
                if (response.Succeeded && response.Data != null)
                {
                    await _auditService.LogAsync("Invoice", response.Data.Id, AuditActions.CREATE,
                        Request.Path, Request.Method,
                        newValues: JsonSerializer.Serialize(response.Data));
                }

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating invoice");
                return ErrorResponse("Failed to create invoice", 500);
            }
        }

        /// <summary>
        /// Update existing invoice
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InvoiceUpdateResponse), 200)]
        public async Task<IActionResult> UpdateInvoice(string id, [FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Get old values for audit trail
                var oldInvoice = await _invoiceService.GetByIdAsync(id);
                string? oldValues = oldInvoice.Succeeded ? JsonSerializer.Serialize(oldInvoice.Data) : null;

                var response = await _invoiceService.UpdateAsync(id, invoiceDto);

                // Log the UPDATE action
                if (response.Succeeded && response.Data != null)
                {
                    await _auditService.LogAsync("Invoice", id, AuditActions.UPDATE,
                        Request.Path, Request.Method,
                        oldValues: oldValues,
                        newValues: JsonSerializer.Serialize(response.Data));
                }

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating invoice {InvoiceId}", id);
                return ErrorResponse("Failed to update invoice", 500);
            }
        }

        /// <summary>
        /// Delete invoice
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(InvoiceDeleteResponse), 200)]
        public async Task<IActionResult> DeleteInvoice(string id)
        {
            try
            {
                // Get invoice data before deletion for audit trail
                var existingInvoice = await _invoiceService.GetByIdAsync(id);
                string? oldValues = existingInvoice.Succeeded ? JsonSerializer.Serialize(existingInvoice.Data) : null;

                var response = await _invoiceService.DeleteAsync(id);

                // Log the DELETE action
                if (response.Succeeded)
                {
                    await _auditService.LogAsync("Invoice", id, AuditActions.DELETE,
                        Request.Path, Request.Method,
                        oldValues: oldValues);
                }

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice {InvoiceId}", id);
                return ErrorResponse("Failed to delete invoice", 500);
            }
        }
    }
}