
namespace hitsLab.presentation.dto.validation.Model
{
    public enum DishSortingModel
    {
        NameAsc, 
        NameDesc, 
        PriceAsc, 
        PriceDesc, 
        RatingAsc, 
        RatingDesc
    }
    
    public static class DishSortingMapper 
    {
        public static string? ToString(DishSortingModel model)
        {
            return model switch
            {
                DishSortingModel.NameAsc => "NameAsc",
                DishSortingModel.NameDesc => "NameDesc",
                DishSortingModel.PriceAsc => "PriceAsc",
                DishSortingModel.PriceDesc => "PriceDesc",
                DishSortingModel.RatingAsc => "RatingAsc",
                DishSortingModel.RatingDesc => "RatingDesc",
                _ => null
            };
        }
    }
}

