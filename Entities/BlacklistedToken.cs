namespace AdminHubApi.Entities;

public class BlacklistedToken
{
    public int Id { get; set; }

    public string TokenId { get; set; } // JWT ID (jti claim)

    public DateTime ExpiryDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}