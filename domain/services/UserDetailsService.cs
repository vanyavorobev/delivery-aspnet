
using System.Web.Helpers;
using hitsLab.data.entity;
using hitsLab.data.repository;
using hitsLab.domain.exception.auth;
using hitsLab.domain.exception.database;
using hitsLab.presentation.dto;

namespace hitsLab.domain.services
{
    public interface IUserDetailsService
    {
        public bool CheckUserCredentials(LoginCredentials credentials);
        public void LoadUserByGuid(Guid id);
        public void LoadUserByEmail(string email);
        public void LoadUserByPhone(string phone);
        public bool IsPermissionEnough();
        public Task CreateUser(UserRegisterDto dto);
        public Task EditUserDetails(UserEditDto dto);
        public UserDto GetUserDetails();
        public Guid GetUserGuid();
    }
    
    public class UserDetailsService : IUserDetailsService
    {
        private IDbRepository _repository { get; set; }
        private UserEntity? _userDetails { get; set; }

        public UserDetailsService(IDbRepository repository)
        {
            _repository = repository;
        }

        public bool CheckUserCredentials(LoginCredentials credentials)
        {
            var user = _repository.FindActiveBy<UserEntity>(entity => entity.Email == credentials.Email)
                .FirstOrDefault() ?? throw new AuthorizationException();
            return Crypto.VerifyHashedPassword(user.Password, credentials.Password);
        }
        
        public void LoadUserByGuid(Guid id)
        {
            _userDetails = _repository.FindActiveBy<UserEntity>(entity => entity.Id == id).FirstOrDefault() 
                          ?? throw new UserNotFoundException();
        }

        public void LoadUserByEmail(string email)
        {
            _userDetails = _repository.FindActiveBy<UserEntity>(entity => entity.Email == email).FirstOrDefault() 
                          ?? throw new UserNotFoundException();
        }

        public void LoadUserByPhone(string phone)
        {
            _userDetails = _repository.FindActiveBy<UserEntity>(entity => entity.PhoneNumber == phone).FirstOrDefault() 
                          ?? throw new UserNotFoundException();
        }

        public bool IsPermissionEnough()
        {
            throw new NotImplementedException();
        }

        public async Task CreateUser(UserRegisterDto dto)
        {
            await _repository.Save(new UserEntity(dto.FullName, dto.BirthDate.ToString("yyyy-MM-ddTHH:mm:ss.ms", System.Globalization.CultureInfo.InvariantCulture), 
                dto.Gender, dto.PhoneNumber, dto.Email,
                dto.Address?? "", Crypto.HashPassword(dto.Password)));
            await _repository.SaveChanges();
        }

        public UserDto GetUserDetails()
        {
            _userDetails = _userDetails 
                           ?? throw new UserNotFoundException();
            return new UserDto(_userDetails.Id, _userDetails.FullName, DateTime.Parse(_userDetails.BirthDate?? ""), _userDetails.Gender,
                _userDetails.Address, _userDetails.Email, _userDetails.PhoneNumber);
        }

        public async Task EditUserDetails(UserEditDto dto)
        {
            _userDetails = _userDetails 
                           ?? throw new UserNotFoundException();
            _userDetails.FullName = dto.FullName;
            _userDetails.BirthDate = dto.BirthDate.ToString("yyyy-MM-ddTHH:mm:ss.ms", System.Globalization.CultureInfo.InvariantCulture);
            _userDetails.Gender = dto.Gender;
            _userDetails.Address = dto.Address?? "";
            _userDetails.PhoneNumber = dto.PhoneNumber;
            await _repository.Update(_userDetails);
            await _repository.SaveChanges();
        }

        public Guid GetUserGuid()
        {
            return _userDetails?.Id 
                   ?? throw new UserNotFoundException();
        }
    }    
}
