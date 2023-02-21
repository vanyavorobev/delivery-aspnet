using System.ComponentModel.DataAnnotations.Schema;

namespace hitsLab.data.entity
{
    [Table("order")]
    public class OrderEntity: BaseEntity
    {
        [ForeignKey("OrderId")]
        public ICollection<DishInOrderEntity>? DishInOrderEntities { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public UserEntity? UserEntity { get; set; }

        public string DeliveryTime { get; set; }
        public string OrderTime { get; set; }
        public double Amount { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public OrderEntity(Guid userId, string deliveryTime, string orderTime, double amount, string address, string status)
        {
            UserId = userId;
            DeliveryTime = deliveryTime;
            OrderTime = orderTime;
            Amount = amount;
            Address = address;
            Status = status;
        }
    }
}