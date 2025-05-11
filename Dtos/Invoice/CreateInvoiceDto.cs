using AdminHubApi.Dtos.InvoiceItem;
using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Dtos.Invoice;

public class CreateInvoiceDto
{
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Notes { get; set; }
    public string? OrderId { get; set; }
    public string UserId { get; set; }
    public decimal PaidAmount { get; set; }
    public InvoiceStatus Status { get; set; }
    public string CreatedById { get; set; }
    public List<CreateInvoiceItemDto> Items { get; set; } = new List<CreateInvoiceItemDto>();
}