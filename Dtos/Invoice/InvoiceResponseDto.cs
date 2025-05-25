using AdminHubApi.Dtos.InvoiceItem;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Dtos.Invoice;

public class InvoiceResponseDto
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public InvoiceStatus Status { get; set; }
    public string Notes { get; set; }
    public Guid? OrderId { get; set; } = Guid.Empty;
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<InvoiceItemResponseDto> Items { get; set; } = new List<InvoiceItemResponseDto>();
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerAddress { get; set; }
    public string BillingAddress { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? TaxRate { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string PaymentTerms { get; set; }
    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public UserDto CreatedBy { get; set; }
    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public UserDto ModifiedBy { get; set; }
}