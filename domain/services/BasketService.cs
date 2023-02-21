
using hitsLab.data.entity;
using hitsLab.data.repository;
using hitsLab.domain.exception.database;
using hitsLab.presentation.dto;

namespace hitsLab.domain.services
{
    public interface IBasketService
    {
        List<DishBasketDto> GetUserCart();
        Task AddDishToCart(string dishId);
        Task DeleteDishFromCart(string dishId);
        Task DecreaseDishFromCart(string dishId);
    }
    public class BasketService : IBasketService
    {
        private readonly IDbRepository _repository;
        private readonly IUserDetailsService _userDetailsService;
        private readonly IDishService _dishService;

        public BasketService(IDbRepository repository, IUserDetailsService userDetailsService, IDishService dishService)
        {
            _repository = repository;
            _userDetailsService = userDetailsService;
            _dishService = dishService;
        }

        public List<DishBasketDto> GetUserCart()
        {
            var userId = _userDetailsService.GetUserGuid();
            var dishes = _repository.FindActiveBy<DishInOrderEntity>(entity => entity.UserId == userId).ToList();
            return dishes.Select(MapEntityToDishBasketDto).ToList();
        }

        public async Task AddDishToCart(string dishId)
        {
            await UpdateCountOfDishInCart(dishId, 1);
        }

        public async Task DeleteDishFromCart(string dishId)
        {
            await UpdateCountOfDishInCart(dishId, -FindDishInCart(dishId)?.Count ?? throw new DishNotFoundException());
        }

        public async Task DecreaseDishFromCart(string dishId)
        {
            await UpdateCountOfDishInCart(dishId, -1);
        }
        
        private async Task UpdateCountOfDishInCart(string dishId, int delta)
        {
            var dish = FindDishInCart(dishId);
            if (dish != null)
            {
                dish.Count += delta;
                if (dish.Count <= 0) await _repository.Delete(dish);
                else await _repository.Update(dish);
            }
            else if (delta > 0)
            {
                await _repository.Save(new DishInOrderEntity(_userDetailsService.GetUserGuid(), null, Guid.Parse(dishId), 1));
            }
            await _repository.SaveChanges();
        }

        private DishInOrderEntity? FindDishInCart(string dishId)
        {
            var userId = _userDetailsService.GetUserGuid();
            var dish = _dishService.FindDishById(dishId);
            return _repository.FindActiveBy<DishInOrderEntity>(entity => entity.UserId == userId && entity.DishId == dish.Id)
                    .FirstOrDefault();
        }

        private DishBasketDto MapEntityToDishBasketDto(DishInOrderEntity baseEntity)
        {
            var dish = _dishService.GetDishInfo(baseEntity.DishId.ToString());
            return new DishBasketDto(baseEntity.DishId, dish.Name, dish.Price, 
                baseEntity.Count * dish.Price, baseEntity.Count, dish.Image);
        }
    }
}