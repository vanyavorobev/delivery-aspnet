using hitsLab.domain.exception.validation;
using hitsLab.domain.services;
using hitsLab.presentation.dto;
using hitsLab.presentation.dto.validation.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hitsLab.presentation.controller
{
    [Route("/api/dish")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        
        [ValidateModel]
        [HttpGet, Route("")]
        public ActionResult GetListOfDishes([FromQuery] GetListOfDishesCriterion criterion)
        {
            if (criterion.Page == null) return BadRequest();
            foreach (var it in criterion.Categories.Where(it => !CategoryMapper.ExistInModel(it)))
            {
                return BadRequest("Категории с именем " + it + " не существует");
            }
            if (criterion.Page <= 0) throw new InvalidPageValueException();
            return Ok(_dishService.GetPageOfDishes(criterion.Page??-1, criterion.Categories, criterion.Vegetarian, criterion.Sorting));
        }

        [HttpGet, Route("{dishId}")] 
        public ActionResult GetDishInfo(string dishId) 
        {
            return Ok(_dishService.GetDishInfo(dishId));
        }

        [Authorize]
        [HttpGet, Route("{dishId}/rating/check")]
        public ActionResult IsAbleToSetRating(string dishId)
        {
            return Ok(_dishService.IsAbleToSetRating(dishId));
        }

        [Authorize]
        [HttpPost, Route("{dishId}/rating")]
        public async Task<ActionResult> SetRating(string dishId, [FromQuery] int ratingScore)
        {
            if (!(0 <= ratingScore && ratingScore <= 10)) throw new InvalidRatingValueException();
            await _dishService.SetRating(dishId, ratingScore);
            return Ok();
        }
    }
}