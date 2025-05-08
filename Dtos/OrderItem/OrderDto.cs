namespace AdminHubApi.Dtos.OrderItem;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedById { get; set; }
    public DateTime Modified { get; set; }
    public string? ModifiedById { get; set; }
}