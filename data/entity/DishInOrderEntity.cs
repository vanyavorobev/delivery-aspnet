using System.ComponentModel.DataAnnotations.Schema;

namespace hitsLab.data.entity
{
    [Table("dishInOrder")]
    public class DishInOrderEntity: BaseEntity
    {
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        [ForeignKey("OrderId")]
        public Guid? OrderId { get; set; }
        public OrderEntity? Order { get; set; }
        [ForeignKey("DishId")]
        public Guid DishId { get; set; }
        public DishEntity? Dish { get; set; }


        public int Count { get; set; } = 0;

        public DishInOrderEntity(Guid userId, Guid? orderId, Guid dishId, int count)
        {
            UserId = userId;
            OrderId = orderId;
            DishId = dishId;
            Count = count;
        }
    }
}