
using hitsLab.presentation.dto.validation.Attribute;

namespace hitsLab.presentation.dto
{
    public class LoginCredentials
    {
        [Email]
        public string Email { get; set; }
        [Password]
        public string Password { get; set; }

        public LoginCredentials(string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}