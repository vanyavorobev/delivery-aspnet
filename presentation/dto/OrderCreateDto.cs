
using System.ComponentModel.DataAnnotations;

namespace hitsLab.presentation.dto
{
    public class OrderCreateDto
    {
        [Timestamp]
        public DateTime DeliveryTime {get; set; }
        [MinLength(1)]
        public string Address {get; set; }

        public OrderCreateDto(DateTime deliveryTime, string address) 
        {
            DeliveryTime = deliveryTime;
            Address = address;
        }
    }
}