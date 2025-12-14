using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.NotFoundExceptions
{
    public class BasketNotFoundException(string id) :
        NotFoundException($"The Basket With Id {id} Not Found")
    {

    }
}
