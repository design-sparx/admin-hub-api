using AdminHubApi.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdminHubApi.Services;

public class TokenCleanupService : BackgroundService
{
    private readonly ILogger<TokenCleanupService> _logger;
    private readonly IServiceProvider _services;
    private readonly TimeSpan _interval = TimeSpan.FromHours(1);

    public TokenCleanupService(
        ILogger<TokenCleanupService> logger,
        IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Token Cleanup Service running");

        using var timer = new PeriodicTimer(_interval);

        while (!stoppingToken.IsCancellationRequested && 
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await CleanupTokensAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cleaning up tokens");
            }
        }
    }

    private async Task CleanupTokensAsync()
    {
        _logger.LogInformation("Cleaning up expired blacklisted tokens");

        using var scope = _services.CreateScope();
        var tokenBlacklistRepository = scope.ServiceProvider.GetRequiredService<ITokenBlacklistRepository>();

        await tokenBlacklistRepository.CleanupExpiredTokensAsync();
    }
}