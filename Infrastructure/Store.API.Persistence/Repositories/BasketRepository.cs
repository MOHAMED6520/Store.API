using StackExchange.Redis;
using Store.API.Domain.Contracts;
using Store.API.Domain.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.API.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

    

        public async Task<CustomerBasket?> GetBasketAsynk(string id)
        {
           var redisValue =await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) return null;
           var basket =  JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if (basket is null) return null;
            return basket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsynk(CustomerBasket basket, TimeSpan? timeToList = null)
        {
            var redisbasket = JsonSerializer.Serialize(basket);
            var flag =await _database.StringSetAsync(basket.Id, redisbasket, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsynk(basket.Id) : null;
        }
        public async Task<bool> DeleteBasketAsynk(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
