using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Orders;

public class UpdateOrderDto
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public string ModifiedById { get; set; }
}