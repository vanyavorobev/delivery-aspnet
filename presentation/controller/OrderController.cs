using hitsLab.domain.services;
using hitsLab.presentation.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hitsLab.presentation.controller
{
    [Route("/api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [Authorize]
        [HttpGet, Route("{orderId}")]
        public ActionResult GetOrderInfo(string orderId) 
        {
            return Ok(_orderService.GetOrderInfo(orderId));
        }
        
        [Authorize]
        [HttpGet, Route("")] 
        public ActionResult GetAllOrders() 
        {
            return Ok(_orderService.GetAllOrdersByUser());
        }

        [ValidateModel]
        [Authorize]
        [HttpPost, Route("")]
        public async Task<ActionResult> CreateOrder([FromBody] OrderCreateDto requestDto)
        {
            await _orderService.CreateOrder(requestDto);
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("{orderId}/status")]
        public async Task<ActionResult> ConfirmOrderDelivery(string orderId)
        {
            await _orderService.ConfirmOrderDelivery(orderId);
            return Ok();
        }
    }
}