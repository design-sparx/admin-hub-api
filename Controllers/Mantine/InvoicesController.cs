using AdminHubApi.Constants;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/invoices")]
    [Tags("Mantine - Invoices")]
    [PermissionAuthorize(Permissions.Personal.Invoices)]
    public class InvoicesController : MantineBaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService, ILogger<InvoicesController> logger)
            : base(logger)
        {
            _invoiceService = invoiceService;
        }

        /// <summary>
        /// Get all invoices with pagination and filtering
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices([FromQuery] InvoiceQueryParams queryParams)
        {
            try
            {
                var invoices = await _invoiceService.GetAllAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = invoices.Data,
                    message = "Invoices retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = invoices.Meta
                });
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
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            try
            {
                var invoice = await _invoiceService.GetByIdAsync(id);
                if (invoice == null)
                    return NotFound(new { success = false, message = "Invoice not found" });

                return SuccessResponse(invoice, "Invoice retrieved successfully");
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
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var invoice = await _invoiceService.CreateAsync(invoiceDto);
                return SuccessResponse(invoice, "Invoice created successfully");
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
        public async Task<IActionResult> UpdateInvoice(string id, [FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var invoice = await _invoiceService.UpdateAsync(id, invoiceDto);
                if (invoice == null)
                    return NotFound(new { success = false, message = "Invoice not found" });

                return SuccessResponse(invoice, "Invoice updated successfully");
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
        public async Task<IActionResult> DeleteInvoice(string id)
        {
            try
            {
                var deleted = await _invoiceService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Invoice not found" });

                return SuccessResponse(new { }, "Invoice deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice {InvoiceId}", id);
                return ErrorResponse("Failed to delete invoice", 500);
            }
        }
    }
}