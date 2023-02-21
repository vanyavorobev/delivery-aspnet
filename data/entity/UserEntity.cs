using System.ComponentModel.DataAnnotations.Schema;

namespace hitsLab.data.entity
{
    [Table("user")]
    public class UserEntity: BaseEntity
    {
        [ForeignKey("UserId")]
        public ICollection<DishInOrderEntity>? DishInOrderEntities { get; set; }
        [ForeignKey("UserId")]
        public ICollection<RatingEntity>? RatingEntities { get; set; }
        [ForeignKey("UserId")]
        public ICollection<OrderEntity>? OrderEntities { get; set; }

        public string FullName { get; set; }
        public string? BirthDate { get; set; }
        public string Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public UserEntity(string fullName, string? birthDate, string gender, string? phoneNumber, string email, string address, string password)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            Password = password;
        }
    }
}