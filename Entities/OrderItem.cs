using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
    
    public DateTime Created { get; set; }
    public string CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    
    public DateTime Modified { get; set; }
    public string ModifiedById { get; set; }
    public ApplicationUser ModifiedBy { get; set; }
}