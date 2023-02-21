
using System.Diagnostics;

namespace hitsLab.presentation.dto.validation.Model
{
    public enum CategoryModel
    {
        Wok, 
        Pizza, 
        Soup, 
        Dessert, 
        Drink
    }

    public static class CategoryMapper
    {
        public static string? ToString(CategoryModel model)
        {
            return model switch
            {
                CategoryModel.Wok => "Wok",
                CategoryModel.Pizza => "Pizza",
                CategoryModel.Soup => "Soup",
                CategoryModel.Dessert => "Dessert",
                CategoryModel.Drink => "Drink",
                _ => null
            };
        }
        
        public static CategoryModel ToModel(string model)
        {
            return model switch
            {
                "Wok" => CategoryModel.Wok,
                "Pizza" => CategoryModel.Pizza,
                "Soup" => CategoryModel.Soup,
                "Dessert" => CategoryModel.Dessert,
                "Drink" => CategoryModel.Drink,
                _ => CategoryModel.Wok
            };
        }

        public static bool ExistInModel(string model)
        {
            return model switch
            {
                "Wok" => true,
                "Pizza" => true,
                "Soup" => true,
                "Dessert" => true,
                "Drink" => true,
                _ => false
            };
        } 
    }
}
