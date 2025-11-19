using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdLiveAuction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string NftName { get; set; } = string.Empty;

        public string NftImage { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SellerUsername { get; set; } = string.Empty;

        [MaxLength(100)]
        public string BuyerUsername { get; set; } = string.Empty;

        public decimal StartPrice { get; set; }

        public decimal EndPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        public bool IsHighestBidMine { get; set; }

        public decimal WinningBid { get; set; }

        [MaxLength(50)]
        public string TimeLeft { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
