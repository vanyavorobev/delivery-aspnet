
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto
{
    public class OrderDto
    {
        public Guid Id {get; set; } 
        [Timestamp]
        public DateTime DeliveryTime {get; set;}
        [Timestamp]
        public DateTime OrderTime {get; set; }
        [EnumDataType(typeof(OrderStatusModel), ErrorMessage = "Такого статуса не существует")]
        public string Status {get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative")]
        public double Price {get; set; }
        public List<DishBasketDto> Dishes {get; set; }
        [MinLength(1)]
        public string Address {get; set; }

        public OrderDto(Guid id, DateTime deliveryTime, DateTime orderTime, string status, double price, List<DishBasketDto> dishes, string address)
        {
            Id = id;
            DeliveryTime = deliveryTime;
            OrderTime = orderTime;
            Status = status;
            Price = price;
            Dishes = dishes;
            Address = address;
        }
    }
}