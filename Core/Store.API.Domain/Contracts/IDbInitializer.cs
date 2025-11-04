using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Contracts
{
    public interface IDbInitializer
    {
        public Task IntializeAsync();
        public Task IntializeIdentityAsync();

    }
}
