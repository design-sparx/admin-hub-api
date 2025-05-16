namespace AdminHubApi.Dtos.InvoiceItem;

public class CreateInvoiceItemDto
{
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int? ProductId { get; set; }
}