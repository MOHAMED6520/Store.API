using Microsoft.AspNetCore.Mvc;
using Store.API.Services.Abstractions;
using Store.API.Shared.Dtos.Basket;
using Store.API.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManger serviceManger): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result =await serviceManger.BasketService.GetBasketAsynk(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketDto basket)
        {
            var result = await serviceManger.BasketService.UpdateBasketAsynk(basket);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await serviceManger.BasketService.DeleteBasketAsynk(id);
            return NoContent();
        }
    }
}
