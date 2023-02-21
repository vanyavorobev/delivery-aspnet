using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.helpers
{
    public static class PasswordValidator
    {
        private const string PatternUpperCase = @".*[A-Z].*";
        private const string PatternLowerCase = @".*[a-z].*";
        private const string PatternContainsDigit = @".*[\d].*";
        private const string PatternContainsWhiteSpace = @"^[\S]+$";
        private const int MinLength = 8;
        private const int MaxLength = 30;

        public static string? Execute(string password)
        {
            string? error = null;
            if (IsPasswordEmpty(password))
            {
                error = "Заполните пароль";
            }
            else if (!IsPasswordDigitValid(password))
            {
                error = "Пароль должен содержать цифры";
            }
            else if (!IsPasswordLengthValid(password))
            {
                error = "Длина пароля должна быть от 8 до 30 символов";
            }
            else if (!IsPasswordLowerCaseValid(password))
            {
                error = "Пароль должен содержать строчные латинские буквы";
            }
            else if (!IsPasswordUpperCaseValid(password))
            {
                error = "Пароль должен содержать заглавные латинские буквы";
            }
            else if (!IsPasswordWhiteSpaceValid(password))
            {
                error = "Пароль не должен содержать пробелы";
            }
            if (error.IsNullOrEmpty())
            {
                error = null;
            }
            return error;
        }

        private static bool IsPasswordEmpty(string password)
        {
            return password.IsNullOrEmpty();
        }

        private static bool IsPasswordUpperCaseValid(string password)
        {
            return Regex.IsMatch(password, PatternUpperCase);
        }

        private static bool IsPasswordLowerCaseValid(string password)
        {
            return Regex.IsMatch(password, PatternLowerCase);
        }

        private static bool IsPasswordDigitValid(string password)
        {
            return Regex.IsMatch(password, PatternContainsDigit);
        }

        private static bool IsPasswordWhiteSpaceValid(string password)
        {
            return Regex.IsMatch(password, PatternContainsWhiteSpace);
        }

        private static bool IsPasswordLengthValid(string password)
        {
            return MinLength <= password.Length && password.Length <= MaxLength;
        }
    }    
}
