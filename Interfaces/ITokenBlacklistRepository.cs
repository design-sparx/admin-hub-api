namespace AdminHubApi.Interfaces;

public interface ITokenBlacklistRepository
{
    Task<bool> IsTokenBlacklistedAsync(string tokenId);
    Task BlacklistTokenAsync(string tokenId, DateTime expiryDate);
    Task CleanupExpiredTokensAsync();
}