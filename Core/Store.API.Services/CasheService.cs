using Store.API.Domain.Contracts;
using Store.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Services
{
    public class CasheService(ICacheRepository _repository) : ICasheService
    {
        public async Task<string?> GetCasheValueAsync(string Key)
        {
           var result = await _repository.GetAsync(Key);
            return result == null ? null : result;
        }

        public async Task SetCasheValueAsync(string Key, object value, TimeSpan duration)
        {
           await _repository.SetAsync(Key, value, duration);
        }
    }
}
