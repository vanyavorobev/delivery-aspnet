
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto 
{
    public class OrderInfoDto
    {
        public Guid Id {get; set; }
        [Timestamp]
        public DateTime DeliveryTime {get; set; }
        [Timestamp]
        public DateTime OrderTime {get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative")]
        public double Price {get; set; }
        [EnumDataType(typeof(OrderStatusModel), ErrorMessage = "Такого статуса не существует")]
        public string Status { get; set; }

        public OrderInfoDto(Guid id, DateTime deliveryTime, DateTime orderTime, double price, string status)
        {
            Id = id;
            DeliveryTime = deliveryTime;
            OrderTime = orderTime;
            Price = price;
            Status = status;
        }
    }
}