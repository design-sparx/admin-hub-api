namespace AdminHubApi.Entities;

public class Order
{
    public Guid Id { get; set; }

    // Optional link to registered user
    public string CustomerId { get; set; }
    public ApplicationUser Customer { get; set; }

    // Order-specific customer information
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public string ShippingAddress { get; set; }

    public string BillingAddress { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }

    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public ApplicationUser ModifiedBy { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}