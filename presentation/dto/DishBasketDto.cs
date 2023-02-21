
using System.ComponentModel.DataAnnotations;

namespace hitsLab.presentation.dto 
{
    public class DishBasketDto 
    {
        public Guid Id { get; set; }
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Имя содержит недопустимые символы")]
        [MinLength(1)]
        public string Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public double Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public double TotalPrice { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int Amount { get; set; }
        public string? Image { get; set; }
        
        public DishBasketDto(Guid id, string name, double price, double totalPrice, int amount, string? image) 
        {
            Id = id;
            Name = name;
            Price = price;
            TotalPrice = totalPrice;
            Amount = amount;
            Image = image;
        }
    }
}
