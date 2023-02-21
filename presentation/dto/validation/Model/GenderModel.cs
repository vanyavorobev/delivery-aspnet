
namespace hitsLab.presentation.dto.validation.Model
{
    public enum GenderModel
    {
        Male, 
        Female
    }

    public static class GenderMapper
    {
        public static string? ToString(GenderModel model)
        {
            return model switch
            { 
                GenderModel.Male => "Male",
                GenderModel.Female => "Female",
                _ => null
            };
        }
        
        public static GenderModel ToModel(string model)
        {
            return model switch
            { 
                "Male" => GenderModel.Male,
                "Female" => GenderModel.Female,
                _ => GenderModel.Male
            };
        }
    }
}
