using Store.API.Shared.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services.Abstractions.Basket
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsynk(string id);
        Task<BasketDto?> UpdateBasketAsynk(BasketDto basket);
        Task<bool> DeleteBasketAsynk(string id);
    }
}
