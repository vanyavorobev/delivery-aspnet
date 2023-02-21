
using System.ComponentModel.DataAnnotations;
using hitsLab.presentation.dto.validation.Model;

namespace hitsLab.presentation.dto;

public class GetListOfDishesCriterion
{
    public int? Page { get; set; }
    public List<string> Categories { get; set; } = new();
    [EnumDataType(typeof(DishSortingModel))]
    public string? Sorting { get; set; } = "";
    public bool Vegetarian { get; set; } = false;
    
    public GetListOfDishesCriterion() {}

    public GetListOfDishesCriterion(int page, List<string> categories, string? sorting, bool vegetarian)
    {
        Page = page;
        Categories = categories;
        Sorting = sorting;
        Vegetarian = vegetarian;
    }
}