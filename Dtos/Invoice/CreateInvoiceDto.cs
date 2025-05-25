using AdminHubApi.Dtos.InvoiceItem;
using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Dtos.Invoice;

public class CreateInvoiceDto
{
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Notes { get; set; }
    public Guid? OrderId { get; set; } // Make nullable
    public string UserId { get; set; }
    public decimal PaidAmount { get; set; }
    public InvoiceStatus Status { get; set; }
    public string CreatedById { get; set; }
    
    // Add missing fields that are in your payload
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerAddress { get; set; }
    public string BillingAddress { get; set; }
    public decimal? TaxRate { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string PaymentTerms { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    
    public List<CreateInvoiceItemDto> Items { get; set; } = new List<CreateInvoiceItemDto>();
}