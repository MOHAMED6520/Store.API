using Store.API.Domain.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsynk(string id);
        Task<CustomerBasket?> UpdateBasketAsynk(CustomerBasket basket , TimeSpan? timeToList = null);
        Task<bool> DeleteBasketAsynk(string id );
    }
}
