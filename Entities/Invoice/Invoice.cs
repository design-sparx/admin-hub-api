using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Invoice;

public enum InvoiceStatus
{
    Draft,
    Sent,
    Paid,
    PartiallyPaid,
    Overdue,
    Cancelled,
    Refunded
}

public class Invoice
{
    public Guid Id { get; set; }

    [Required] public string InvoiceNumber { get; set; }

    [Required] public DateTime IssueDate { get; set; }

    [Required] public DateTime DueDate { get; set; }

    [Required] public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    [Required] public InvoiceStatus Status { get; set; } // Draft, Sent, Paid, Overdue, Cancelled

    public string Notes { get; set; }

    // Foreign key for related order
    public Guid OrderId { get; set; }

    [ForeignKey("OrderId")] public Order Order { get; set; }

    // Foreign key for customer/user
    public string UserId { get; set; }

    [ForeignKey("UserId")] public ApplicationUser User { get; set; }

    // Collection of invoice items
    public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    
    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public ApplicationUser ModifiedBy { get; set; }
}