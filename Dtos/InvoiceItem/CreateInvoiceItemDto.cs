using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.InvoiceItem;

public class CreateInvoiceItemDto
{
    [Required] public string Description { get; set; }
    [Required] public decimal Quantity { get; set; }
    [Required] public decimal UnitPrice { get; set; }
    [Required] public decimal TotalPrice { get; set; }
    public Guid? ProductId { get; set; }
}