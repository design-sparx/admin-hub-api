using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Dtos.Invoice;

public class UpdateInvoiceDto
{
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal PaidAmount { get; set; }
    public string Notes { get; set; }
    public InvoiceStatus Status { get; set; }
    public string ModifiedById { get; set; }
}