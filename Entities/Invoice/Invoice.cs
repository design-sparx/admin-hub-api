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

    [Required] public InvoiceStatus Status { get; set; }

    public string Notes { get; set; }

    // Make OrderId optional - invoices can be standalone or linked to orders
    public Guid? OrderId { get; set; }  // Changed to nullable

    [ForeignKey("OrderId")] public Order Order { get; set; }

    // Customer information (since invoices can be standalone)
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerAddress { get; set; }
    public string BillingAddress { get; set; }
    
    // Payment and pricing details
    public decimal? Subtotal { get; set; }
    public decimal? TaxRate { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string PaymentTerms { get; set; }

    // Foreign key for customer/user (also make optional)
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