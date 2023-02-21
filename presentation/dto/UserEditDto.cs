
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto
{
    public class UserEditDto
    {
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Name contains invalid symbols")]
        [MinLength(1)]
        public string FullName {get; set; }
        [Timestamp]
        public DateTime BirthDate {get; set; }
        [EnumDataType(typeof(GenderModel), ErrorMessage = "Такого гендера не существует")]
        public string Gender {get; set; }
        public string? Address {get; set; }
        [validation.Attribute.Phone]
        public string? PhoneNumber {get; set; }

        public UserEditDto(string fullName, string? address, DateTime birthDate, string gender, string? phoneNumber)
        {
            FullName = fullName;
            Address = address;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }
    }
}