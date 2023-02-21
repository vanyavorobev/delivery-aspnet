using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.helpers
{
    public static class EmailValidator
    {
        private const string PatternEmail = @"^[\w\d\-\.]+@[\w\d\-\.]+\.[\w\d\-\.]+$";
        private const string PatternAt = @"@";
        private const int BodyMinLength = 0;
        private const int BodyMaxLength = 320;
        private const int DomainMinLength = 2;
        private const int DomainMaxLength = 63;

        public static string? Execute(string email)
        {
            if (IsEmailEmpty(email))
            {
                return "Укажите адрес электронной почты";
            }
            else if (!IsEmailLengthValid(email))
            {
                return "Недопустимая длина электронного адреса";
            }
            else if (!IsEmailBodyValid(email))
            {
                return "Недопустимый формат электронного адреса";
            }
            else if (!IsEmailDomainValid(email))
            {
                return "Недопустимая длина домена";
            }
            else
            {
                return null;
            }
        }

        private static bool IsEmailEmpty(string email)
        {
            return email.IsNullOrEmpty();
        }

        private static bool IsEmailLengthValid(string email)
        {
            return BodyMinLength < email.Length && email.Length <= BodyMaxLength;
        }

        private static bool IsEmailBodyValid(string email)
        {
            return Regex.IsMatch(email, PatternEmail);
        }

        private static bool IsEmailDomainValid(string email)
        {
            var domain = email[Regex.Match(email, PatternAt).Index..];
            return DomainMinLength <= domain.Length && domain.Length <= DomainMaxLength;
        }
    }    
}