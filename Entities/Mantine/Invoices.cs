using AdminHubApi.Enums.Mantine;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Mantine
{
    public class Invoices
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        public InvoiceStatus Status { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        public DateTime IssueDate { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string ClientEmail { get; set; } = string.Empty;

        [Required]
        public string ClientAddress { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ClientCountry { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string ClientCompany { get; set; } = string.Empty;

        [MaxLength(450)]
        public string? CreatedById { get; set; }

        [MaxLength(255)]
        public string? CreatedByEmail { get; set; }

        [MaxLength(255)]
        public string? CreatedByName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey(nameof(CreatedById))]
        public ApplicationUser? CreatedBy { get; set; }
    }
}