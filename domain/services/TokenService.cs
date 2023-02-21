
using System.IdentityModel.Tokens.Jwt;
using hitsLab.data.entity;
using hitsLab.data.repository;
using hitsLab.domain.exception.validation;
using hitsLab.domain.helpers;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.services
{
    public interface ITokenService
    {
        public bool IsTokenActive(string token, string type);
        public string? GetUsernameFromToken(string token);
        public Task RemoveToken(string token);
        public Task<string> GenerateAccessToken(string username);
        public Task<string> GenerateRefreshToken(string username);
    }

    public class TokenService : ITokenService
    {
        private readonly IDbRepository _repository;

        public TokenService(IDbRepository repository)
        {
            _repository = repository;
        }
        
        public bool IsTokenActive(string token, string type)
        {
            var t = _repository.FindBy<TokenEntity>(entity => entity.Token == token).FirstOrDefault();
            if (t == null) return false;
            return DateTime.Parse(t.DateCreated) >= DateTime.UtcNow - TimeSpan.FromMinutes(GetTokenTime(type));
        }

        public string? GetUsernameFromToken(string token)
        {
            return new JwtSecurityToken(token).Payload["username"].ToString();
        }

        public async Task RemoveToken(string token)
        {
            await _repository.DeleteBy<TokenEntity>(entity => entity.Token == token);
            await _repository.SaveChanges();
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var token = new JwtSecurityTokenHandler().WriteToken(
                GenerateToken(TimeSpan.FromMinutes(TokenConfig.AccessTokenLifetime), username));
            await SaveToken(token);
            return token;
        }

        public async Task<string> GenerateRefreshToken(string username)
        {
            var token = new JwtSecurityTokenHandler().WriteToken(
                GenerateToken(TimeSpan.FromMinutes(TokenConfig.RefreshTokenLifetime), username));
            await SaveToken(token, TokenType.Refresh);
            return token;
        }

        private async Task SaveToken(string token, TokenType type = TokenType.Access)
        {
            await _repository.Save(new TokenEntity(token, GetTokenType(type)));
            await _repository.SaveChanges();
        }
        
        private static string GetTokenType(TokenType type)
        {
            return type switch
            {
                TokenType.Access => "Access",
                TokenType.Refresh => "Refresh",
                _ => throw new InvalidTokenTypeException()
            };
        }
        
        private static TokenType GetTokenType(string type)
        {
            return type switch
            {
                "Access" => TokenType.Access,
                "Refresh" => TokenType.Refresh,
                _ => throw new InvalidTokenTypeException()
            };
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
        
        private static JwtSecurityToken GenerateToken(TimeSpan lifetime, string username)
        {
            var token = new JwtSecurityToken(
                issuer: TokenConfig.Issuer,
                audience: TokenConfig.Audience,
                claims: null,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(lifetime),
                signingCredentials: new SigningCredentials(TokenConfig.GetSymmetricalSecurityKey(), TokenConfig.GetAlgorithm()));
            token.Payload.Add("username", username);
            return token;
        }
    }
}
