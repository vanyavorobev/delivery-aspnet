
using hitsLab.data.entity;
using hitsLab.data.repository;
using hitsLab.domain.exception.validation;
using hitsLab.domain.helpers;

namespace hitsLab.domain.services
{
    public class TokenDaemonService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TokenDaemonService> _logger;
        public TokenDaemonService(
            IServiceProvider serviceProvider,
            ILogger<TokenDaemonService> logger) =>
            (_serviceProvider, _logger) = (serviceProvider, logger);
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await CleanupTokens();
                    await Task.Delay(TimeSpan.FromMinutes(TokenConfig.TokenCleanupDeltaTime), stoppingToken);
                }
            }, stoppingToken);
            return Task.CompletedTask;
        }

        private async Task CleanupTokens()
        {
            using var scope = _serviceProvider.CreateScope();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            var repository = scope.ServiceProvider.GetRequiredService<IDbRepository>();
            var tokens = repository.FindAll<TokenEntity>().ToList();
            foreach (var token in tokens.Where(t => DateTime.Parse(t.DateCreated) <= DateTime.UtcNow - TimeSpan.FromMinutes(GetTokenTime(t.Type))))
            {
                await tokenService.RemoveToken(token.Token);
            }
        }
        
        
        private static int GetTokenTime(TokenType type)
        {
            return type switch
            {
                TokenType.Access => TokenConfig.AccessTokenLifetime,
                TokenType.Refresh => TokenConfig.RefreshTokenLifetime,
                _ => throw new InvalidTokenTypeException()
            };
        }
        
        private static int GetTokenTime(string type)
        {
            return type switch
            {
                "Access" => TokenConfig.AccessTokenLifetime,
                "Refresh" => TokenConfig.RefreshTokenLifetime,
                _ => throw new InvalidTokenTypeException()
            };
        }
    }
}
