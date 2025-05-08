namespace AdminHubApi.Dtos.OrderItem;

public class OrderItemResponseDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}