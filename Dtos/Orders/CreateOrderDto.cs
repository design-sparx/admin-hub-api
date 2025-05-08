using AdminHubApi.Dtos.OrderItem;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Orders;

public class CreateOrderDto
{
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    public string CreatedById { get; set; }
}