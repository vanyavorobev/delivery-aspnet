using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.helpers
{
    public static class PhoneValidator
    {
        private const string PatternPhone = @"^[\d\-+\s()]+$";
        private const string PatternPlus = @"[+]";
        private const string PatternBrackets = @"[()]";
        private const string PatternMinus = @"[\-]";
        private const string PatternWhiteSpace = @"[\s]";
        private const int ValidLength = 11;
            
        public static string? Execute(string phone)
        {
            if (IsPhoneEmpty(phone))
            {
                return "Укажите номер телефона";
            }
            else if(!IsPhoneValid(phone))
            {
                return "Недопустимый символ в номере";
            }
            else if (!IsPhoneLengthValid(phone))
            {
                return "Заполните номер до конца";
            }
            else
            {
                return null;
            }
        }

        private static bool IsPhoneEmpty(string phone)
        {
            return phone.IsNullOrEmpty();
        }
        private static bool IsPhoneValid(string phone)
        {
            return Regex.IsMatch(phone, PatternPhone);
        }
        private static bool IsPhoneLengthValid(string phone)
        {
            return ValidLength == GetPhoneLength(phone);
        }
        
        private static int GetPhoneLength(string phone)
        {
            var validLength = phone.Length;
            validLength -= Regex.Count(phone, PatternPlus);
            validLength -= Regex.Count(phone, PatternBrackets);
            validLength -= Regex.Count(phone, PatternWhiteSpace);
            validLength -= Regex.Count(phone, PatternMinus);
            return validLength;
        }
    }
}

