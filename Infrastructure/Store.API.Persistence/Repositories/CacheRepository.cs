using StackExchange.Redis;
using Store.API.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.API.Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var result =await _database.StringGetAsync(key);
            return result.IsNullOrEmpty ? result : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var rediesValue = JsonSerializer.Serialize(value);
           await _database.StringSetAsync(key, rediesValue, duration);
        }
    }
}
