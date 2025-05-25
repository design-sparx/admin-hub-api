namespace AdminHubApi.Dtos.InvoiceItem;

public class InvoiceItemResponseDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid? ProductId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}