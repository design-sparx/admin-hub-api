using AdminHubApi.Dtos.OrderItem;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Orders;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public ICollection<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();
    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public string CreatedByName { get; set; }
    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public string ModifiedByName { get; set; }
}