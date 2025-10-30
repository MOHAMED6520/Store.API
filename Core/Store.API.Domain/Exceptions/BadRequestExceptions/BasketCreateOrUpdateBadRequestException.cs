using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.BadRequestExceptions
{
    public class BasketCreateOrUpdateBadRequestException(): 
        BadRequestException("Invalid Operation When Create Or Update Basket")
    {
    }
}
