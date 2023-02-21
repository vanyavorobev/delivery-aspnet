
using hitsLab.domain.exception.auth;
using hitsLab.domain.exception.validation;
using hitsLab.presentation.dto;

namespace hitsLab.domain.services
{
    public interface IAuthService
    {
        Task<TokenResponse> RegisterUser(UserRegisterDto requestDto);
        Task<TokenResponse> LoginUser(LoginCredentials requestDto);
        Task LogoutUser(string token);
    }
    
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserDetailsService _userDetailsService;

        public AuthService(ITokenService tokenService, IUserDetailsService userDetailsService)
        {
            _tokenService = tokenService;
            _userDetailsService = userDetailsService;
        }
        
        public async Task<TokenResponse> RegisterUser(UserRegisterDto requestDto) 
        {
            if (requestDto.BirthDate <= DateTime.Parse("1900-01-01T00:00:00.000Z") || DateTime.UtcNow <= requestDto.BirthDate) throw new ValidationException();
            await _userDetailsService.CreateUser(requestDto);
            var token = new TokenResponse(await _tokenService.GenerateAccessToken(requestDto.Email));
            return token;
        }

        public async Task<TokenResponse> LoginUser(LoginCredentials requestDto) 
        {
            if(!_userDetailsService.CheckUserCredentials(requestDto)) throw new LoginFailedException();
            var token = new TokenResponse(await _tokenService.GenerateAccessToken(requestDto.Email));
            return token;
        }

        public async Task LogoutUser(string token)
        {
            if (token.Contains("Bearer ")) token = token.Remove(0, "Bearer ".Length);
            await _tokenService.RemoveToken(token);
        }
    }    
}
