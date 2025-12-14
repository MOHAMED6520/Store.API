using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.BadRequestExceptions
{
    public class BasketDeleteBadRequestException():BadRequestException($"Invalid Operation When Delete Basket")
    {
    }
}
