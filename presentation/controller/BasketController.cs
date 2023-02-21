using hitsLab.domain.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hitsLab.presentation.controller
{
    [Route("/api/basket")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        
        [Authorize]
        [HttpGet, Route("")]
        public ActionResult GetUserCart() 
        {
            return Ok(_basketService.GetUserCart());
        }

        [Authorize]
        [HttpPost, Route("dish/{dishId}")] 
        public async Task<ActionResult> AddDishToCart(string dishId)
        {
            await _basketService.AddDishToCart(dishId);
            return Ok();
        }

        [Authorize]
        [HttpDelete, Route("dish/{dishId}")]
        public async Task<ActionResult> DeleteDishFromCart(string dishId, [FromQuery]bool increase)
        {
            if (increase)
            {
                await _basketService.DecreaseDishFromCart(dishId);
            }
            else
            {
                await _basketService.DeleteDishFromCart(dishId);
            }
            return Ok();
        }
    }
}