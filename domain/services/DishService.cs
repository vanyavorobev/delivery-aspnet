
using hitsLab.data.entity;
using hitsLab.data.repository;
using hitsLab.domain.exception.database;
using hitsLab.domain.exception.forbidden;
using hitsLab.presentation.dto;
using hitsLab.presentation.dto.validation.Model;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.services
{
    public interface IDishService
    {
        DishPagedListDto GetPageOfDishes(int page, List<string> categories, bool vegetarian, string? sorting);
        DishDto GetDishInfo(string dishId);
        bool IsAbleToSetRating(string dishId);
        Task SetRating(string dishId, double rating);
        DishEntity FindDishById(string dishId);
    }

    public class DishService : IDishService
    {
        private const int PageSize = 5;
        private readonly IDbRepository _repository;
        private readonly IUserDetailsService _userDetailsService;

        public DishService(IDbRepository repository, IUserDetailsService userDetailsService)
        {
            _repository = repository;
            _userDetailsService = userDetailsService;
        }
        
        public DishPagedListDto GetPageOfDishes(int page, List<string> categories, bool vegetarian, string? sorting)
        {
            var allDishes = _repository.
                FindActiveBy<DishEntity>(it => it.IsVegetarian == vegetarian || it.IsVegetarian == true)
                .Where(it => categories.Contains(it.Category) || categories.IsNullOrEmpty()).AsEnumerable().Select(MapEntityToDishDto).ToList();
            if (allDishes.Count <= PageSize * (page - 1)) throw new PageNotFoundException();
            allDishes = Sorting(sorting, allDishes);

            var dishesOnPage = allDishes.GetRange(PageSize * (page - 1), 
                Math.Min(PageSize, allDishes.Count - PageSize * (page-1)));
            
            if (dishesOnPage.Count == 0) throw new PageNotFoundException();
            return new DishPagedListDto(dishesOnPage, new PageInfoDto(dishesOnPage.Count, 
                (int)Math.Ceiling((double)allDishes.Count/PageSize), page));
        }

        private static List<DishDto> Sorting(string? sorting, List<DishDto> list)
        {
            if(sorting == DishSortingMapper.ToString(DishSortingModel.NameAsc)) list.Sort((first, second) => string.Compare(first.Name, second.Name, StringComparison.Ordinal)); 
            if(sorting == DishSortingMapper.ToString(DishSortingModel.NameDesc)) list.Sort((first, second) => string.Compare(second.Name, first.Name, StringComparison.Ordinal)); 
            if(sorting == DishSortingMapper.ToString(DishSortingModel.PriceAsc)) list.Sort((first, second) => first.Price.CompareTo(second.Price)); 
            if(sorting == DishSortingMapper.ToString(DishSortingModel.PriceDesc)) list.Sort((first, second) => second.Price.CompareTo(first.Price)); 
            if(sorting == DishSortingMapper.ToString(DishSortingModel.RatingAsc)) list.Sort((first, second) => CompareRating(first.Rating, second.Rating)); 
            if(sorting == DishSortingMapper.ToString(DishSortingModel.RatingDesc)) list.Sort((first, second) => CompareRating(second.Rating, first.Rating));
            
            return list;
        }

        private static int CompareRating(double? a, double? b)
        {
            if (a < b)
                return -1;
            if (a > b)
                return 1;
            if (a == b)
                return 0;
            if (a == null)
                return 1;
            return b == null ? -1 : 0;
        }

        public DishDto GetDishInfo(string dishId)
        {
            var entity = FindDishById(dishId);
            return MapEntityToDishDto(entity);
        }

        public bool IsAbleToSetRating(string dishId)
        {
            var userId = _userDetailsService.GetUserGuid();
            var dish = FindDishById(dishId);
            return _repository
                .FindBy<DishInOrderEntity>(entity => entity.DishId == dish.Id && entity.UserId == userId && entity.OrderId != null)
                .FirstOrDefault() != null;
        }

        public async Task SetRating(string dishId, double rating)
        {
            var userId = _userDetailsService.GetUserGuid();
            if (!IsAbleToSetRating(dishId)) throw new ForbiddenToSetRatingException();
            var ratingEntity = FindRatingByDishId(dishId);
            if (ratingEntity == null)
            {
                await _repository.Save(new RatingEntity(userId, Guid.Parse(dishId), rating));
            }
            else
            {
                ratingEntity.Rating = rating;
                await _repository.Update(ratingEntity);
            }
            await _repository.SaveChanges();
        }

        private RatingEntity? FindRatingByDishId(string dishId)
        {
            var userId = _userDetailsService.GetUserGuid();
            var dish = FindDishById(dishId);
            var rating = _repository
                .FindActiveBy<RatingEntity>(entity => entity.DishId == dish.Id && entity.UserId == userId)
                .FirstOrDefault();
            return rating;
        }

        public DishEntity FindDishById(string dishId)
        {
            return _repository.FindActiveBy<DishEntity>(entity => entity.Id == Guid.Parse(dishId))
                       .FirstOrDefault() ?? throw new DishNotFoundException();
        }
        
        private static DishDto MapEntityToDishDto(DishEntity entity)
        {
            return new DishDto(entity.Id, entity.Name, entity.Description, entity.Price, entity.ImageLink,
                entity.IsVegetarian, entity.GetAverageRating(), entity.Category);
        }
    }
}
