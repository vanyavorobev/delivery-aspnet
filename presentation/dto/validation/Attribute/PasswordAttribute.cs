using System.ComponentModel.DataAnnotations;
using hitsLab.domain.helpers;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.presentation.dto.validation.Attribute
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string str)
            {
                var error = PasswordValidator.Execute(str);
                if(!error.IsNullOrEmpty()) ErrorMessage = error!;
                return error.IsNullOrEmpty();
            }
            return false;
        }
    }
}
