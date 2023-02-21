
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Attribute;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto
{
    public class UserDto
    {
        public Guid Id {get; set; }
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Name contains invalid symbols")]
        [MinLength(1)]
        public string FullName {get; set; }
        [Timestamp]
        public DateTime? BirthDate {get; set; }
        [EnumDataType(typeof(GenderModel), ErrorMessage = "Такого гендера не существует")]
        public string Gender {get; set; }
        public string? Address {get; set; }
        [Email]
        public string? Email {get; set; }
        [validation.Attribute.Phone]
        public string? PhoneNumber {get; set; }

        public UserDto(Guid id, string fullName, DateTime? birthDate, string gender, string? address, string? email, string? phoneNumber)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}