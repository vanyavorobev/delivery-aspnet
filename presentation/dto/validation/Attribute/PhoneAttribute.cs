using System.ComponentModel.DataAnnotations;
using hitsLab.domain.helpers;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.presentation.dto.validation.Attribute
{
    public class PhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string str)
            {
                var errors = PhoneValidator.Execute(str);
                ErrorMessage = errors;
                return errors.IsNullOrEmpty();
            }

            return false;
        }
    }
}