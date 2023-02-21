
using Azure.Core;
using Microsoft.OpenApi.Attributes;

namespace hitsLab.presentation.dto 
{
    public class DishPagedListDto
    {
        public List<DishDto> Dishes { get; set; }
        public PageInfoDto Pagination { get; set; }

        public DishPagedListDto(List<DishDto> dishes, PageInfoDto pagination) 
        {
            Dishes = dishes;
            Pagination = pagination;
        }
    }
}