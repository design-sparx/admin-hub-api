using AdminHubApi.Dtos.InvoiceItem;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities.Invoice;

namespace AdminHubApi.Dtos.Invoice;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public InvoiceStatus Status { get; set; }
    public string Notes { get; set; }
    public Guid OrderId { get; set; } = Guid.Empty;
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<InvoiceItemDto> Items { get; set; } = new List<InvoiceItemDto>();
    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public UserDto CreatedBy { get; set; }
    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public UserDto ModifiedBy { get; set; }
}