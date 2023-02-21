
using hitsLab.data.entity;
using hitsLab.data.repository;
using hitsLab.domain.exception.database;
using hitsLab.domain.exception.forbidden;
using hitsLab.domain.exception.validation;
using hitsLab.presentation.dto;
using Microsoft.IdentityModel.Tokens;

namespace hitsLab.domain.services;

public interface IOrderService
{
    OrderDto GetOrderInfo(string orderId);
    List<OrderInfoDto> GetAllOrdersByUser();
    Task CreateOrder(OrderCreateDto requestDto);
    Task ConfirmOrderDelivery(string orderId);
}
    
public class OrderService : IOrderService
{
    private readonly IDbRepository _repository;
    private readonly IUserDetailsService _userDetailsService;
    private readonly IDishService _dishService;

    public OrderService(IDbRepository repository, IUserDetailsService userDetailsService, IDishService dishService)
    {
        _repository = repository;
        _userDetailsService = userDetailsService;
        _dishService = dishService;
    }
        
    public OrderDto GetOrderInfo(string orderId)
    {
        return MapEntityToOrderDto(FindOrderById(orderId));
    }

    public List<OrderInfoDto> GetAllOrdersByUser()
    {
        var userId = _userDetailsService.GetUserGuid();
        return _repository.FindBy<OrderEntity>(entity => entity.UserId == userId)
            .AsEnumerable().Select(MapEntityToOrderInfoDto).ToList();
    }

    public async Task CreateOrder(OrderCreateDto requestDto)
    {
        var dishes = GetDishesFromBasket();
        var userId = _userDetailsService.GetUserGuid();
        var deltaTime = requestDto.DeliveryTime - DateTime.UtcNow;
        if (TimeSpan.FromHours(1) >= deltaTime) throw new InvalidDeliveredTimeException();
        var order = new OrderEntity(userId, requestDto.DeliveryTime.ToString("yyyy-MM-ddTHH:mm:ss.ms", System.Globalization.CultureInfo.InvariantCulture), 
            DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ms", System.Globalization.CultureInfo.InvariantCulture),
            CalculateAmount(dishes), requestDto.Address, "InProcess");
        foreach (var dish in dishes)
        {
            dish.OrderId = order.Id;
        }
        await _repository.Save(order);
        await _repository.ChangeAllActivity(dishes);
        await _repository.SaveChanges();
    }

    public async Task ConfirmOrderDelivery(string orderId)
    {
        var userId = _userDetailsService.GetUserGuid();
        var order = FindOrderById(orderId);
        if (userId != order.UserId) throw new ForbiddenResourceException();
        if (order.Status != "Delivered")
        {
            order.Status = "Delivered";
            await _repository.Update(order);
            await _repository.SaveChanges();
        }
    }

    private double CalculateAmount(List<DishInOrderEntity> dishes)
    {
        return (from it in dishes let dish = _dishService.GetDishInfo(it.DishId.ToString()) select dish.Price * it.Count).Sum();
    }

    private List<DishInOrderEntity> GetDishesFromBasket()
    {
        var userId = _userDetailsService.GetUserGuid();
        var dishes = _repository.FindActiveBy<DishInOrderEntity>(entity => entity.UserId == userId).ToList();
        if (dishes.IsNullOrEmpty()) throw new OrderNotFoundException();
        return dishes;
    }

    private OrderEntity FindOrderById(string orderId)
    {
        var userId = _userDetailsService.GetUserGuid();
        var order = _repository.FindActiveBy<OrderEntity>(entity => entity.Id == Guid.Parse(orderId))
            .FirstOrDefault() ?? throw new OrderNotFoundException();
        if (order.UserId != userId) throw new ForbiddenResourceException();
        return order;
    }

    private OrderDto MapEntityToOrderDto(OrderEntity entity)
    {
        var dishes = _repository.FindBy<DishInOrderEntity>(it => it.OrderId == entity.Id).ToList();
        return new OrderDto(entity.Id, DateTime.Parse(entity.DeliveryTime), DateTime.Parse(entity.OrderTime),
            entity.Status, entity.Amount, 
            dishes.AsEnumerable().Select(MapEntityToDishBasketDto).ToList(), entity.Address);
    }

    private DishBasketDto MapEntityToDishBasketDto(DishInOrderEntity entity)
    {
        var dish = _dishService.GetDishInfo(entity.DishId.ToString());
        return new DishBasketDto(dish.Id, dish.Name, dish.Price, entity.Count * dish.Price, entity.Count,
            dish.Image);
    }

    private static OrderInfoDto MapEntityToOrderInfoDto(OrderEntity entity)
    {
        return new OrderInfoDto(entity.Id, DateTime.Parse(entity.DeliveryTime), DateTime.Parse(entity.OrderTime),
            entity.Amount, entity.Status);
    }
}