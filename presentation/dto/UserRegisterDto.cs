
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Attribute;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto
{
    public class UserRegisterDto
    {
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Name contains invalid symbols")]
        [MinLength(1)]
        public string FullName {get; set; }
        [Password]
        public string Password {get; set; }
        [Email]
        public string Email {get; set; }
        public string? Address {get; set; }
        [Timestamp]
        public DateTime BirthDate {get; set; }
        [EnumDataType(typeof(GenderModel), ErrorMessage = "Такого гендера не существует")]
        public string Gender {get; set; }
        [validation.Attribute.Phone]
        public string? PhoneNumber {get; set; }

        public UserRegisterDto(string fullName, string password, string email, string? address, DateTime birthDate, string gender, string? phoneNumber)
        {
            FullName = fullName;
            Password = password;
            Email = email;
            Address = address;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }
    }
}