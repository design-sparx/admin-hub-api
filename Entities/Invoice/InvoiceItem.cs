using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Invoice;

public class InvoiceItem
{
    public Guid Id { get; set; }

    [Required] public string Description { get; set; }

    [Required] public decimal Quantity { get; set; }

    [Required] public decimal UnitPrice { get; set; }

    [Required] public decimal TotalPrice { get; set; }

    // Foreign key for a related product (optional)
    public Guid ProductId { get; set; }

    [ForeignKey("ProductId")] public Product Product { get; set; }

    // Foreign key for invoice
    public Guid InvoiceId { get; set; }

    [ForeignKey("InvoiceId")] public Invoice Invoice { get; set; }
    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    
    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public ApplicationUser ModifiedBy { get; set; }
}