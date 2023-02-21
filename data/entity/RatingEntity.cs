using System.ComponentModel.DataAnnotations.Schema;

namespace hitsLab.data.entity
{
    [Table("rating")]
    public class RatingEntity: BaseEntity
    {

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }

        [ForeignKey("DishId")]
        public Guid DishId { get; set; }
        public DishEntity? Dish { get; set; }

        public double Rating { get; set; }

        public RatingEntity(Guid userId, Guid dishId, double rating)
        {
            UserId = userId;
            DishId = dishId;
            Rating = rating;
        }
    }
}