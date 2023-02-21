
using System.ComponentModel.DataAnnotations.Schema;

namespace hitsLab.data.entity
{
    [Table("dish")]
    public class DishEntity: BaseEntity
    {
        [ForeignKey("DishId")]
        public ICollection<DishInOrderEntity>? DishesInOrder { get; set; }
        [ForeignKey("DishId")]
        public ICollection<RatingEntity>? Ratings { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public bool IsVegetarian { get; set; }
        public string? ImageLink { get; set; }
        public string Category { get; set; }

        public DishEntity(string name, double price, string? description, bool isVegetarian, string? imageLink, string category)
        {
            Name = name;
            Price = price;
            Description = description;
            IsVegetarian = isVegetarian;
            ImageLink = imageLink;
            Category = category;
        }

        public double? GetAverageRating()
        {
            if (Ratings == null || Ratings.Count == 0) return null;
            return Ratings.Sum(it => it.Rating) / Ratings.Count;
        }
    }
}
