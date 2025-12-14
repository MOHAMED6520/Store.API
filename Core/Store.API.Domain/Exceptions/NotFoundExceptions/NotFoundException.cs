using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.API.Domain.Exceptions.NotFoundExceptions
{
    public class NotFoundException(string message) :Exception(message)
    {
      
    }
}
