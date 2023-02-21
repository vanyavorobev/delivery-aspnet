
using hitsLab.domain.exception.validation;
using hitsLab.presentation.dto;

namespace hitsLab.domain.services
{
    public interface IUserService
    {
        UserDto GetUserProfile();
        Task EditUserProfile(UserEditDto requestDto);
    }
    
    public class UserService : IUserService
    {
        private readonly IUserDetailsService _userDetailsService;

        public UserService(IUserDetailsService userDetailsService)
        {
            _userDetailsService = userDetailsService;
        }

        public UserDto GetUserProfile()
        {
            return _userDetailsService.GetUserDetails();
        }  

        public async Task EditUserProfile(UserEditDto requestDto)
        {
            if (requestDto.BirthDate <= DateTime.Parse("1900-01-01T00:00:00.000Z") || DateTime.UtcNow <= requestDto.BirthDate) throw new ValidationException();
            await _userDetailsService.EditUserDetails(requestDto);
        }
    }
}
