using AutoMapper;
using Store.API.Domain.Contracts;
using Store.API.Domain.Exceptions.BadRequestExceptions;
using Store.API.Domain.Exceptions.NotFoundExceptions;
using Store.API.Domain.Models.Basket;
using Store.API.Services.Abstractions.Basket;
using Store.API.Shared.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public class BasketServices(IBasketRepository _basket,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsynk(string id)
        {
           var basket =await _basket.GetBasketAsynk(id);
            if (basket is null) throw new BasketNotFoundException(id);
           var result = _mapper.Map<BasketDto>(basket);

            return result;
        }

        public async Task<BasketDto?> UpdateBasketAsynk(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            basket = await _basket.UpdateBasketAsynk(basket);
            if (basket is null) throw new BasketCreateOrUpdateBadRequestException();
            var result = _mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<bool> DeleteBasketAsynk(string id)
        {
           var result=await _basket.DeleteBasketAsynk(id);
            if (!result) throw new BasketDeleteBadRequestException();
            return result;
        }

      
    }
}
