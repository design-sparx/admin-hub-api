namespace AdminHubApi.Dtos.InvoiceItem;

public class InvoiceItemDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int? ProductId { get; set; }
    public string ProductName { get; set; }
}