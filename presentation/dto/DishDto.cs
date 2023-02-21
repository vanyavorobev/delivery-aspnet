
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto 
{
    public class DishDto 
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Имя содержид недопустимые символы")]
        [MinLength(1)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public double Price { get; set; }
        public string? Image { get; set; }
        public bool Vegetarian { get; set; }
        [Range(0, 10, ErrorMessage = "Рейтинг должен находится на промежутке 0 - 10")]
        public double? Rating { get; set; }
        [EnumDataType(typeof(CategoryModel), ErrorMessage = "Такой категории не существует")]
        public string Category { get; set; }
        
        public DishDto(Guid id, string name, string? description, double price, string? image, bool vegetarian, double? rating, string category) 
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            Vegetarian = vegetarian;
            Rating = rating;
            Category = category;
        }
    }
}
