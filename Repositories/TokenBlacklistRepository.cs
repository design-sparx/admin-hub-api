using AdminHubApi.Data;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Repositories;

public class TokenBlacklistRepository : ITokenBlacklistRepository
{
    private readonly ApplicationDbContext _context;

    public TokenBlacklistRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> IsTokenBlacklistedAsync(string tokenId)
    {
        return await _context.BlacklistedTokens
            .AnyAsync(t => t.TokenId == tokenId);
    }

    public async Task BlacklistTokenAsync(string tokenId, DateTime expiryDate)
    {
        if (await _context.BlacklistedTokens.AnyAsync(t => t.TokenId == tokenId))
            return;
            
        var blacklistedToken = new BlacklistedToken
        {
            TokenId = tokenId,
            ExpiryDate = expiryDate,
            CreatedAt = DateTime.UtcNow
        };
        
        await _context.BlacklistedTokens.AddAsync(blacklistedToken);
        await _context.SaveChangesAsync();
    }

    public async Task CleanupExpiredTokensAsync()
    {
        var now = DateTime.UtcNow;
        
        var expiredTokens = await _context.BlacklistedTokens
            .Where(t => t.ExpiryDate < now)
            .ToListAsync();
            
        if (expiredTokens.Any())
        {
            _context.BlacklistedTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync();
        }
    }
}