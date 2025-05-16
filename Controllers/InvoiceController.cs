using AdminHubApi.Constants;
using AdminHubApi.Dtos.Invoice;
using AdminHubApi.Entities.Invoice;
using AdminHubApi.Security;
using AdminHubApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/invoices")]
[PermissionAuthorize(Permissions.Products.View)]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceService _invoiceService;
    private readonly ILogger<InvoicesController> _logger;

    public InvoicesController(InvoiceService invoiceService, ILogger<InvoicesController> logger)
    {
        _invoiceService = invoiceService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllInvoices()
    {
        var invoices = await _invoiceService.GetAllAsync();

        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetInvoiceById(Guid id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);

        return Ok(invoice);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetInvoicesByUserId(string userId)
    {
        var invoices = await _invoiceService.GetByUserIdAsync(userId);

        return Ok(invoices);
    }

    [HttpGet("order/{orderId}")]
    public async Task<ActionResult> GetInvoicesByOrderId(string orderId)
    {
        var invoices = await _invoiceService.GetByOrderIdAsync(orderId);

        return Ok(invoices);
    }

    [HttpPost]
    [PermissionAuthorize(Permissions.Products.Create)]
    public async Task<ActionResult> CreateInvoice(CreateInvoiceDto createInvoiceDto)
    {
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = createInvoiceDto.InvoiceNumber,
            IssueDate = createInvoiceDto.IssueDate,
            DueDate = createInvoiceDto.DueDate,
            Notes = createInvoiceDto.Notes,
            OrderId = createInvoiceDto.OrderId,
            UserId = createInvoiceDto.UserId,
            Status = createInvoiceDto.Status,
            PaidAmount = createInvoiceDto.PaidAmount,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedById = createInvoiceDto.CreatedById,
            ModifiedById = createInvoiceDto.CreatedById
        };

        var result = await _invoiceService.CreateAsync(invoice);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, result);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize(Permissions.Products.Edit)]
    public async Task<IActionResult> UpdateInvoice(Guid id, UpdateInvoiceDto updateInvoiceDto)
    {
        var invoiceResponse = await _invoiceService.GetByIdAsync(id);

        if (!invoiceResponse.Succeeded)
        {
            return NotFound(invoiceResponse);
        }

        var existingInvoice = invoiceResponse.Data;
        existingInvoice.InvoiceNumber = updateInvoiceDto.InvoiceNumber;
        existingInvoice.IssueDate = updateInvoiceDto.IssueDate;
        existingInvoice.DueDate = updateInvoiceDto.DueDate;
        existingInvoice.PaidAmount = updateInvoiceDto.PaidAmount;
        existingInvoice.Notes = updateInvoiceDto.Notes;
        existingInvoice.Status = updateInvoiceDto.Status;
        existingInvoice.ModifiedById = updateInvoiceDto.ModifiedById;
        existingInvoice.Modified = DateTime.UtcNow;

        var result = await _invoiceService.UpdateAsync(existingInvoice);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize(Permissions.Products.Delete)]
    public async Task<IActionResult> DeleteInvoice(Guid id)
    {
        var invoiceResponse = await _invoiceService.GetByIdAsync(id);

        if (!invoiceResponse.Succeeded)
        {
            return NotFound(invoiceResponse);
        }

        var result = await _invoiceService.DeleteAsync(id);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}