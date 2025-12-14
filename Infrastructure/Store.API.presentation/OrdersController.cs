using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.API.Services.Abstractions;
using Store.API.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManger _serviceManger) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.orderService.CreateOrderAsync(request, userEmailClaim.Value);
            return Ok(result);
        }

        [HttpGet]
        [Route("DeliveryMethods")]
        public async Task<IActionResult> DeliveryMethods()
        {
            var result = await _serviceManger.orderService.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
        {
            var Email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.orderService.GetOrderByIdForSpecificUserAsync(id, Email.Value);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var Email = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManger.orderService.GetOrdersForSpecificUserAsync( Email.Value);
            return Ok(result);
        }
    }
}
