using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.NotFoundExceptions
{
    public class ProductNotFoundException(int id) :
        NotFoundException($"The Product With Id {id} Not Found")
    {

    }
}
