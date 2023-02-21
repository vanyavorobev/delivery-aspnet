
using hitsLab.domain.exception.auth;
using hitsLab.domain.exception.database;
using hitsLab.domain.services;

namespace hitsLab.presentation.middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private IUserDetailsService? _userDetailsService;
        private ITokenService? _tokenService;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext http, IUserDetailsService userDetailsService, ITokenService tokenService)
        {
            _userDetailsService = userDetailsService;
            _tokenService = tokenService;

            var token = http.Request.Headers.Authorization.ToString();
            AuthorizeUser(token);
            
            await _next.Invoke(http);
        }

        private void AuthorizeUser(string? token)
        {
            if (token == null || !token.Contains("Bearer ")) return;
            token = token.Remove(0, "Bearer ".Length);
            if (!_tokenService!.IsTokenActive(token, "Access")) return;
            var email = _tokenService?.GetUsernameFromToken(token)!;
            Console.WriteLine("email " + email);
            try { _userDetailsService?.LoadUserByEmail(email); } catch(Exception _) {}
        }
    }    
}
