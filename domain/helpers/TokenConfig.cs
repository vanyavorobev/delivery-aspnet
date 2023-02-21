
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.helpers
{
    public static class TokenConfig
    {
        public const int TokenCleanupDeltaTime = 1;
        public const int AccessTokenLifetime = 15;
        public const int RefreshTokenLifetime = 60 * 24 * 7;
        public const string Issuer = "ServerDeliveryApp";
        public const string Audience = "ClientDeliveryApp";
        private const string Key = "MyJwtSuperSecretKey";

        public static SymmetricSecurityKey GetSymmetricalSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public static string GetAlgorithm()
        {
            return SecurityAlgorithms.HmacSha256;
        }
    }
}

